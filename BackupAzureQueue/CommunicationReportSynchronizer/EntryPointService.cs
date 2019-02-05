using Microsoft.IT.RelationshipManagement.Interchange.Platform.Azure.Diagnostics;
using System;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Interchange.Platform.Services.InterchangeCommunicationReportSynchronizer.CommunicationReportSynchronizer
{
    public abstract class EntryPointService : IDisposable
    {
        #region CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the EntryPointService class
        /// </summary>        
        protected EntryPointService()
        {
            /*AzureLog.DiagnosticConnectionString = SecretsManager.Decrypt(ConfigurationManager.AppSettings[Resources.DIAGNOSTICS_CONNECTION_STRING]);
            AzureLog.ConnectionString = SecretsManager.Decrypt(ConfigurationManager.AppSettings[Resources.AZURE_SQL_CONNECTION_STRING]);

            LogType logType = LogType.Verbose;
            string logTypeSetting = ConfigurationManager.AppSettings[Resources.LOG_TYPE_SETTING];
            Enum.TryParse<LogType>(logTypeSetting, out logType);
            AzureLog.LogTypesetting = logType;

            var _smtpHost = ConfigurationManager.AppSettings[Resources.SMTP_HOST];
            var _smtpPort = ConfigurationManager.AppSettings[Resources.SMTP_PORT];
            var _smtpOperationTimeoutInMilliseconds = ConfigurationManager.AppSettings[Resources.SMTP_OPERATION_TIMEOUT_IN_MILLISECONDS];
            var _senderEmailAccount = ConfigurationManager.AppSettings[Resources.SENDER_EMAIL_ACCOUNT];
            var _senderEmailAccountPassword = ConfigurationManager.AppSettings[Resources.SENDER_EMAIL_ACCOUNT_PASSWORD];
            var _isSslEnabled = ConfigurationManager.AppSettings[Resources.IS_SSL_ENABLED];
            var _emailSubject = ConfigurationManager.AppSettings[Resources.EMAIL_SUBJECT];
            var _recipientEmailAccounts = ConfigurationManager.AppSettings[Resources.RECIPIENT_EMAIL_ACCOUNTS];

            string exceptionMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(_smtpHost)) exceptionMessage += "SmtpHost, ";
            if (string.IsNullOrWhiteSpace(_smtpPort)) exceptionMessage += "SmtpPort, ";
            if (string.IsNullOrWhiteSpace(_smtpOperationTimeoutInMilliseconds)) exceptionMessage += "SmtpOperationTimeoutInMilliseconds, ";
            if (string.IsNullOrWhiteSpace(_senderEmailAccount)) exceptionMessage += "SenderEmailAccount, ";
            if (string.IsNullOrWhiteSpace(_senderEmailAccountPassword)) exceptionMessage += "SenderEmailAccountPassword, ";
            if (string.IsNullOrWhiteSpace(_isSslEnabled)) exceptionMessage += "IsSslEnabled, ";
            if (string.IsNullOrWhiteSpace(_emailSubject)) exceptionMessage += "EmailSubject, ";
            if (string.IsNullOrWhiteSpace(_recipientEmailAccounts)) exceptionMessage += "RecipientEmailAccounts, ";

            if (!string.IsNullOrWhiteSpace(exceptionMessage))
                throw new Exception("Smtp configuration setting. Following parameter(s) are having either empty or whitespaces. '" + exceptionMessage.TrimEnd(new[] { ',' }).Trim() + "'");

            Emailer.SmtpHost = ConfigurationManager.AppSettings[Resources.SMTP_HOST];
            Emailer.SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings[Resources.SMTP_PORT]);
            Emailer.SmtpOperationTimeoutInMilliseconds = Convert.ToInt32(ConfigurationManager.AppSettings[Resources.SMTP_OPERATION_TIMEOUT_IN_MILLISECONDS]);
            Emailer.SenderEmailAccount = ConfigurationManager.AppSettings[Resources.SENDER_EMAIL_ACCOUNT];
            Emailer.SenderEmailAccountPassword = SecretsManager.Decrypt(ConfigurationManager.AppSettings[Resources.SENDER_EMAIL_ACCOUNT_PASSWORD]);
            Emailer.IsSslEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings[Resources.IS_SSL_ENABLED]);
            Emailer.EmailSubject = ConfigurationManager.AppSettings[Resources.EMAIL_SUBJECT];
            Emailer.RecipientEmailAccounts = ConfigurationManager.AppSettings[Resources.RECIPIENT_EMAIL_ACCOUNTS];*/
        }
        #endregion

        #region EVENTS
        /// <summary>
        /// Starts the timer which gets requests and pushes those requests to the RequestStore
        /// </summary>
        public void Start()
        {
            Thread mainThread = new Thread(MonitorEntryPoint);
            mainThread.Name = "MainThread";
            mainThread.Start();
        }

        /// <summary>
        /// Stops the timer and makes sure the the service does not stop unless the current processing is complete.
        /// </summary>
        public void Stop()
        {
        }
        #endregion

        #region PUBLIC_METHODS
        /// <summary>
        /// Monitors a particular entry point and creates and pushes requests if any for processing to the Request Store
        /// Error Codes = 2503x
        /// </summary>
        public void MonitorEntryPoint()
        {
            try
            {
                //AzureLog.LogInformation(AzureLog.CreateInformationLogItem(new Guid(), new Guid(), string.Empty, Component.InterchangeMSIFileProcessor, string.Empty, 110002001, "EntryPointService.MonitorEntryPoint", SeverityType.Information, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + Resources.START_MONITORING_AND_PROCESSING, string.Empty, string.Empty, LogType.Verbose, LogClassification.System));
                
                /*
                 * Creating a long-running task for monitoring the sftp channels in 'sliding-window' approach
                 * - It uses 'Parallel.ForEach()' which is variant for 'ThreadPool' class library to encourage parallelism
                 * - It monitors the sftp channels with degree-of-parallelism (dop) as 'm'
                 * - It reuses the threads in threadpool to monitor other sftp channels in queue when they are available
                 */
                Task.Factory.StartNew(() => SyncReportFromDatabase("", "", "", ""), TaskCreationOptions.LongRunning);
                Task.Factory.StartNew(() => SyncReportFromDatabase("", "", "", ""), TaskCreationOptions.LongRunning);
                Task.Factory.StartNew(() => SyncReportFromExchangeServer("", "", "", "", "", "", "", ""), TaskCreationOptions.LongRunning);

                //AzureLog.LogInformation(AzureLog.CreateInformationLogItem(new Guid(), new Guid(), string.Empty, Component.InterchangeMSIFileProcessor, string.Empty, 110002002, "EntryPointService.MonitorEntryPoint", SeverityType.Information, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + Resources.COMPLETED_MONITORING_AND_PROCESSING, string.Empty, string.Empty, LogType.Verbose, LogClassification.System));
            }
            catch (Exception exception)
            {
                //InterchangeEventLog.WriteEntry(exception.Message, System.Diagnostics.EventLogEntryType.Error, 65172);
                //AzureLog.LogException(new Guid(), new Guid(), string.Empty, Component.InterchangeMSIFileProcessor, string.Empty, 111002003, "EntryPointService.MonitorEntryPoint", SeverityType.Exception, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + exception.Message, exception.StackTrace, string.Empty, LogType.Business, LogClassification.System);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SyncReportFromDatabase(string sourceDbConnectionString, string destinationDbConnectionString, string driverXmlFile, string sqlCommandText)
        {
            int servicePollingIntervalInMinutes = 0;
            while (true)
            {
                try
                {
                    //AzureLog.LogInformation(AzureLog.CreateInformationLogItem(new Guid(), new Guid(), string.Empty, Component.InterchangeMSIFileProcessor, string.Empty, 110010001, "EntryPointService.QueueTasksFromTenantsToDatabase", SeverityType.Information, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + "Starting this iteration to get files from tenants and add tasks to database..", string.Empty, string.Empty, LogType.Verbose, LogClassification.System));

                    #region GENESIS
                    var helper = new Helper();
                    var data = helper.GetSqlXmlReport(sourceDbConnectionString, sqlCommandText);

                    var inferredValues = helper.ParseValuesFromXmlDocument(
                                        driverXmlFile
                                        , data.Rows[0][0].ToString());

                    helper.PushValuesFromTextEmailBodyToSqlServer(
                        destinationDbConnectionString
                        , driverXmlFile
                        , inferredValues);
                    #endregion

                    //AzureLog.LogInformation(AzureLog.CreateInformationLogItem(new Guid(), new Guid(), string.Empty, Component.InterchangeMSIFileProcessor, string.Empty, 110010002, "EntryPointService.QueueTasksFromTenantsToDatabase", SeverityType.Information, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + "Completed this iteration by getting (" + noOfFilesPooled + ") files from tenants and added tasks to database within this timeframe..", string.Empty, string.Empty, LogType.Verbose, LogClassification.System));
                }
                catch (Exception exception)
                {
                    //InterchangeEventLog.WriteEntry(exception.Message + ((exception.InnerException != null && !string.IsNullOrWhiteSpace(exception.InnerException.Message)) ? "InnerException: " + exception.InnerException.Message : string.Empty), System.Diagnostics.EventLogEntryType.Error, 65227);
                    //AzureLog.LogException(new Guid(), new Guid(), string.Empty, Component.InterchangeMSIFileProcessor, string.Empty, 111010003, "EntryPointService.QueueTasksFromTenantsToDatabase", SeverityType.Exception, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + exception.Message + ((exception.InnerException != null && !string.IsNullOrWhiteSpace(exception.InnerException.Message)) ? "InnerException: " + exception.InnerException.Message : string.Empty), exception.StackTrace, string.Empty, LogType.Business, LogClassification.System);
                }

                //Common wait time between two runs..
                Thread.Sleep(servicePollingIntervalInMinutes * 60 * 1000);
            }
        }

        /// <summary>
        /// It is the request listener which will continuously be monitoring the supervisor database for new tasks
        /// </summary>
        public void SyncReportFromExchangeServer(string microsoftUsername, string microsoftPassword, string microsoftDomainName, string emailAddress, string emailFolderName, string emailSubject, string destinationDbConnectionString, string driverXmlFile)
        {
            int servicePollingIntervalInMinutes = 0;

            while (true)
            {
                try
                {
                    #region EXACT_TARGET
                    var helper = new Helper();
                    var textEmailBody = helper.GetTextEmailBodyByEmailSubjectFromExchangeServer(microsoftUsername, microsoftPassword, microsoftDomainName, emailAddress, emailFolderName, emailSubject);

                    var dictionary = helper.ParseValuesFromRawTextEmailBody(driverXmlFile, textEmailBody);

                    p.PushValuesFromTextEmailBodyToSqlServer(destinationDbConnectionString, driverXmlFile, dictionary);
                    #endregion
                }
                catch (Exception ex)
                {
                    //InterchangeEventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error, 65262);
                    //AzureLog.LogException(new Guid(), new Guid(), string.Empty, Component.InterchangeMSIFileProcessor, string.Empty, 110011004, "EntryPointService.ProcessFilesFromDatabase", SeverityType.Exception, "Thread - " + "Thread_" + Thread.CurrentThread.ManagedThreadId + "; " + ex.Message, ex.StackTrace, string.Empty, LogType.Business, LogClassification.System);
                }

                //Common wait time between two runs..
                Thread.Sleep(servicePollingIntervalInMinutes * 60 * 1000);
            }
        }

        /// <summary>
        /// Frees all native resources associated with this instance
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ABSTRACT_METHODS
        /// <summary>
        /// Gets a list of requests to be queued for execution in the Request Store
        /// </summary>
        /// <param name="currentProblemTenantName"></param>
        public abstract void MonitorSftpDirectories(out string currentProblemTenantName);
        #endregion
    }
}
