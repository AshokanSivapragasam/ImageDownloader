using HtmlAgilityPack;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Interchange.Platform.Services.InterchangeCommunicationReportSynchronizer.CommunicationReportSynchronizer
{
    public class Helper
    {
        #region PRIVATE_VARIABLES
        string connectionString;
        int maxRetryAttempts;
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Instantiates the members of the class 
        /// </summary>
        public Helper()
        {
            string _connectionString = ConfigurationManager.AppSettings[Resources.AZURE_SQL_CONNECTION_STRING];
            if (string.IsNullOrEmpty(_connectionString))
                throw new ConfigurationErrorsException(Resources.AZURE_SQL_CONNECTION_STRING);
            else connectionString = _connectionString;

            int _maxRetryAttempts = 3;
            int.TryParse(ConfigurationManager.AppSettings[Resources.MAX_RETRY_COUNT], out _maxRetryAttempts);
            if (_maxRetryAttempts == 3)
                maxRetryAttempts = _maxRetryAttempts;
        }
        #endregion

        /// <summary>
        /// Gets xml report from sql server
        /// </summary>
        /// <param name="connectionString"></param>
        public DataTable GetSqlXmlReport(string connectionString, string sqlCommandText)
        {
            var report = new DataTable();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(sqlCommandText, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(report);
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Open))
                    sqlConnection.Close();
            }

            return report;
        }

        /// <summary>
        /// Gets text body of the specific email from MS Exchange Server by Email Subject
        /// </summary>
        /// <param name="microsoftUsername"></param>
        /// <param name="microsoftPassword"></param>
        /// <param name="microsoftDomainName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="emailSubject"></param>
        public string GetTextEmailBodyByEmailSubjectFromExchangeServer(string microsoftUsername, string microsoftPassword, string microsoftDomainName, string emailAddress, string emailFolderName, string emailSubject)
        {
            var textEmailBody = string.Empty;
            ExchangeService exchangeService = null;
            HtmlDocument htmlDocument = new HtmlDocument();

            try
            {
                //Instead of pushing ExchangeService to use specific version of ExchangeServer,
                //let ExchangeService choose the latest available version of Exchange Server
                exchangeService = new ExchangeService();
                exchangeService.Credentials = new NetworkCredential(microsoftUsername, microsoftPassword, microsoftDomainName);
                exchangeService.AutodiscoverUrl(emailAddress);

                Console.WriteLine("Exchange version available for your account: '" + exchangeService.RequestedServerVersion + "'");

                var folderView = new FolderView(1);
                var itemView = new ItemView(1);
                itemView.OrderBy.Add(ItemSchema.DateTimeReceived, Microsoft.Exchange.WebServices.Data.SortDirection.Descending);
                itemView.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);

                var folders = exchangeService.FindFolders(emailFolderName.ToLower().Equals("inbox") ? WellKnownFolderName.MsgFolderRoot : WellKnownFolderName.Inbox, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.IsEqualTo(FolderSchema.DisplayName, emailFolderName)), folderView);
                var folderId = folders.FirstOrDefault().Id;
                var findResults = exchangeService.FindItems(folderId, new SearchFilter.SearchFilterCollection(LogicalOperator.Or, new SearchFilter.ContainsSubstring(ItemSchema.Subject, emailSubject)), itemView);

                foreach (Item item in findResults.Items)
                {
                    if (exchangeService.RequestedServerVersion.ToString().Contains("2013"))
                    {
                        Console.WriteLine("Using native functionality in 'ExchangeService' to get text version of the email");
                        var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body, ItemSchema.TextBody);
                        var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                        textEmailBody = message.TextBody.Text;
                    }
                    else
                    {
                        Console.WriteLine("Using out-of-box functionalities in 'HtmlAgilityPack' to get text version of the email");
                        var propSet = new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.Body);
                        var message = EmailMessage.Bind(exchangeService, item.Id, propSet);
                        htmlDocument.LoadHtml(message.Body);
                        textEmailBody = htmlDocument.DocumentNode.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return textEmailBody;
        }

        /// <summary>
        /// Parse values from raw text email body
        /// </summary>
        /// <param name="exactTargetDriverFile"></param>
        /// <param name="textEmailBody"></param>
        /// <returns></returns>
        public Dictionary<string, string> ParseValuesFromRawTextEmailBody(string exactTargetDriverFile, string textEmailBody)
        {
            #region INIT_VARIABLES
            var xDoc = new XmlDocument();
            xDoc.Load(exactTargetDriverFile);

            //To store the dynamic variables and values
            var dictionary = new Dictionary<string, string>();
            #endregion

            #region GET_PATTERNS_VARIABLES_AND_VALUES_TEXT_EMAIL_BODY
            //Get patterns and variables
            var xNodes = xDoc.SelectNodes("/DriverFile/RegexPatterns/Regex");
            foreach (XmlNode xNode in xNodes)
            {
                if (!dictionary.ContainsKey(xNode.Attributes["Variable"].InnerText))
                {
                    var regex = new Regex(xNode.Attributes["Pattern"].InnerText);
                    var match = regex.Match(textEmailBody);
                    dictionary[xNode.Attributes["Variable"].InnerText] = match.Groups[1].Value;
                }
            }
            #endregion

            return dictionary;
        }

        /// <summary>
        /// Parse values from xml document
        /// </summary>
        /// <param name="genesisDriverFile"></param>
        /// <param name="xmlText"></param>
        /// <returns></returns>
        public Dictionary<string, string> ParseValuesFromXmlDocument(string genesisDriverFile, string xmlText)
        {
            #region INIT_VARIABLES
            var xDocumentDriverFile = new XmlDocument();
            xDocumentDriverFile.Load(genesisDriverFile);
            var xDocumentRawContent = new XmlDocument();
            xDocumentRawContent.LoadXml(xmlText);

            //To store the dynamic variables and values
            var dictionary = new Dictionary<string, string>();
            #endregion

            #region GET_VARIABLES_AND_VALUES_FROM_XML_DOCUMENT
            //Get patterns and variables
            var xNodes = xDocumentDriverFile.SelectNodes("/DriverFile/Datasource/Data");
            foreach (XmlNode xNode in xNodes)
            {
                if (!dictionary.ContainsKey(xNode.Attributes["Variable"].InnerText))
                    dictionary[xNode.Attributes["Variable"].InnerText] = xDocumentRawContent.SelectSingleNode(xNode.Attributes["Xpath"].InnerText).InnerText;
            }
            #endregion

            return dictionary;
        }

        /// <summary>
        /// Pushes the values parsed from text email body to database server
        /// </summary>
        /// <param name="sqlConnectionString"></param>
        /// <param name="driverFile"></param>
        /// <param name="textEmailBody"></param>
        public void PushValuesFromTextEmailBodyToSqlServer(string sqlConnectionString, string driverFile, Dictionary<string, string> dictionary)
        {
            #region INIT_VARIABLES
            var xDoc = new XmlDocument();
            xDoc.Load(driverFile);
            #endregion

            #region SUBSTITUTE_VALUES_TO_REPLACEMENT_STRINGS_IN_SQL_QUERY
            var queryText = xDoc.SelectSingleNode("/DriverFile/SqlCommands").InnerText;
            //Substitute the values to the replacement strings in sql query
            foreach (var variable in dictionary.Keys)
                queryText = queryText.Replace("{" + variable + "}", dictionary[variable]);
            #endregion

            #region RUN_QUERY_TEXT_ON_SQL_SERVER
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(sqlConnectionString);
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(queryText, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Open))
                    sqlConnection.Close();
            }
            #endregion
        }

        /// <summary>
        /// Updates the datetime in database with the epoch time of the latest picked up file for a particular tenant.
        /// </summary>
        /// <param name="tenantName"></param>
        /// <param name="epochDateTimeAsString">The datetime in database with the epoch time of the latest picked up file</param>        
        public void UpdateStatusOfWorkToCommunicationSummarySupervisor(string DataSystemName, DateTime ReportForDate, string DataPullStatus, DateTime DataPullEndDateTime)
        {
            int retryAttempts = 0;

            while (retryAttempts < maxRetryAttempts)
            {
                try
                {
                    int sleepInterval = (int)Math.Pow(2, retryAttempts);
                    using (SqlConnection sqlConnection = new SqlConnection(SecretsManager.Decrypt(connectionString)))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.Connection = sqlConnection;
                            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                            sqlCommand.CommandText = "spUpdateStatusOfWorkToCommunicationSummarySupervisor";

                            SqlParameter paramDataSystemName = new SqlParameter("@DataSystemName", DataSystemName);
                            paramDataSystemName.DbType = System.Data.DbType.String;
                            paramDataSystemName.Direction = ParameterDirection.Input;
                            sqlCommand.Parameters.Add(paramDataSystemName);

                            SqlParameter paramReportForDate = new SqlParameter("@ReportForDate", ReportForDate);
                            paramReportForDate.DbType = System.Data.DbType.Date;
                            paramReportForDate.Direction = ParameterDirection.Input;
                            sqlCommand.Parameters.Add(paramReportForDate);

                            SqlParameter paramDataPullStatus = new SqlParameter("@DataPullStatus", DataPullStatus);
                            paramDataPullStatus.DbType = System.Data.DbType.String;
                            paramDataPullStatus.Direction = ParameterDirection.Input;
                            sqlCommand.Parameters.Add(paramDataPullStatus);

                            SqlParameter paramDataPullEndDateTime = new SqlParameter("@DataPullEndDateTime", DataPullEndDateTime);
                            paramDataPullEndDateTime.DbType = System.Data.DbType.DateTime;
                            paramDataPullEndDateTime.Direction = ParameterDirection.Input;
                            sqlCommand.Parameters.Add(paramDataPullEndDateTime);

                            if (retryAttempts > 0)
                            {
                                Thread.Sleep(sleepInterval * 1000);
                            }

                            sqlCommand.ExecuteNonQuery();

                            //Diagnostics.AzureLog.LogInformation(Diagnostics.AzureLog.CreateInformationLogItem(new Guid(), new Guid(), RequestType.MSIFileData.ToString(), Diagnostics.Component.InterchangeMSIFileProcessor, string.Empty, 111026001, "InterchangeDatabaseWrapper.UpdateProcessingStatusOfFile", Diagnostics.SeverityType.Information, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; Update processing status of msi file, '" + fileName + "' as, '" + processingStatus + "'", string.Empty, string.Empty, Diagnostics.LogType.Verbose, Diagnostics.LogClassification.Transaction));
                        }
                    }
                    break;
                }
                catch (Exception exception)
                {
                    if (retryAttempts == maxRetryAttempts - 1)
                    {
                        //Diagnostics.AzureLog.LogException(new Guid(), new Guid(), RequestType.MSIFileData.ToString(), Diagnostics.Component.InterchangeMSIFileProcessor, string.Empty, 111026002, "InterchangeDatabaseWrapper.UpdateProcessingStatusOfFile", Diagnostics.SeverityType.Exception, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + "Failed to update processing status of msi file, '" + fileName + "' as, '" + processingStatus + "'. Retries exceeded. Exception: " + exception.Message, exception.StackTrace, string.Empty, Diagnostics.LogType.Business, Diagnostics.LogClassification.Transaction);
                        throw;
                    }
                    else
                    {
                        //Diagnostics.AzureLog.LogException(new Guid(), new Guid(), RequestType.MSIFileData.ToString(), Diagnostics.Component.InterchangeMSIFileProcessor, string.Empty, 111026003, "InterchangeDatabaseWrapper.UpdateProcessingStatusOfFile", Diagnostics.SeverityType.Exception, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + "Failed to update processing status of msi file, '" + fileName + "' as, '" + processingStatus + "'. Retrying(" + retryAttempts + ")... Exception: " + exception.Message, exception.StackTrace, string.Empty, Diagnostics.LogType.Business, Diagnostics.LogClassification.Transaction);
                    }
                }
                retryAttempts++;
            }
        }
    }
}
