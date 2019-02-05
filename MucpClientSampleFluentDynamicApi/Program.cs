// <copyright>
//  Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using Microsoft.Membership.Communications.Client;
using Microsoft.Membership.Communications.Common.Delivery;
using Microsoft.Windows.Services.AuthN.Client.S2S;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MucpClientSample
{
    /// <summary>
    /// Sample integration with the MUCP service using the MUCP client SDK
    /// Things to consider:
    /// - Replace the constants with your own values
    /// - Make sure your team has been on-boarded: your cert thumb-print must be white-listed, events need to be configured
    /// - MUCP Client SDK binaries have been copied to the project for simplicity. Make sure you consume the right package from NUGET
    /// </summary>
    public class Program
    {
        #region GLOBAL_VARIABLES
        // TODO: Replace these values with your own 
        private const string PartnerName = "YourAppNameAsRegdInIris"; // Application name or Partner Name as registered in point c
        private const string ExternalKeyOfTriggeredSendDefnUnderThisAccountName = "ExternalKeyOfTriggeredSendDefnUnderThisAccountName"; // Specify External Key of your Triggered Send Definition
        private const string AccountNameForRespectiveSfmcChildAccountAsRegistered = "Name of ET Child Account (without spaces or special chars)"; // Specify Account Name as onboarded in point c 
        private const string CertificateThumprint = "98d8a26c52bff3cf899af7ecf7b88889fc70b7af"; // Your cert thumbprint as registered in MSM Portal
        private const long SiteId = 12345; // Your site Id as generated in MSM Portal

        // These settings should not change unless you go to PROD
        private const string S2SMsaIntAuthenticationEndpoint = "https://login.live-int.com/pksecure/oauth20_clientcredentials.srf"; // RPS endpoint
        private const string MucpEndpoint = "https://mucp.api.account.microsoft-int.com/events/v1/Trigger"; // MUCP endpoint
        private const string MucpTargetSite = "mucp.api.account.microsoft-int.com"; // MUCP target site

        private static Publisher publisher;
        private static S2SAuthClient s2sAuthClient;
        private static ExactTargetDelivery exactTargetDelivery;
        #endregion

        #region MAIN_METHOD
        public static void Main(string[] args)
        {
            #region ..COMMON_INIT
            var program = new Program();
            var topicId = "transactional";
            var validity = TimeSpan.FromDays(2); //For transactional, it has to be 2 days.
            var tenantType = "BULKSEND"; //TBN or BULKSEND
            #endregion

            #region ..COMMON_INITIALIZE_IRIS_API
            program.InitializeIrisApi(certificateThumprint: CertificateThumprint, siteId: SiteId, partnerName: PartnerName, accountNameForRespectiveSfmcChildAccountAsRegistered: AccountNameForRespectiveSfmcChildAccountAsRegistered, externalKeyOfTriggeredSendDefnUnderThisAccountName: ExternalKeyOfTriggeredSendDefnUnderThisAccountName, topicId: topicId, validity: validity);
            #endregion

            #region ..PROCESSING
            switch (tenantType.ToUpper())
            {
                case "TBN":
                    {
                        #region TBN

                        #region ..SEND_THE_TRIGGERED_REQUESTS_TO_SALESFORCE_THRU_IRIS_API
                        program.SendTriggeredRequestToSalesforceThruIrisApi(externalKeyOfTriggeredSendDefnUnderThisAccountName: ExternalKeyOfTriggeredSendDefnUnderThisAccountName);
                        #endregion

                        #endregion

                        break;
                    }
                case "BULKSEND":
                    {
                        #region BULKSEND

                        var bulksendFlatfileName = @"FullPathOfYourBulksendFlatfile";
                        var rowDelimiter = "\r\n";
                        var columnDelimiter = "\t";
                        var dataTable = new DataTable();

                        #region ..READ_THE_BULK_SEND_FLATFILE_AND_DISINTEGRATE_IT_TO_DATATABLE
                        dataTable = program.DisintegrateFlatfileToDatatable(bulksendFlatfileName, rowDelimiter: rowDelimiter, columnDelimiter: columnDelimiter);
                        #endregion

                        #region ..SEND_THE_TRIGGERED_REQUESTS_TO_SALESFORCE_THRU_IRIS_API
                        program.SendTriggeredRequestsToSalesforceThruIrisApi(dataTable: dataTable, externalKeyOfTriggeredSendDefnUnderThisAccountName: ExternalKeyOfTriggeredSendDefnUnderThisAccountName);
                        #endregion

                        #endregion

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Specify right tenant type in source code. It could be either 'TBN' or BULKSEND");
                        break;
                    }
            }
            #endregion
        }
        #endregion

        #region COMMON_METHODS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificateThumprint">Your cert thumbprint as registered in MSM Portal</param>
        /// <param name="siteId">Your site Id as generated in MSM Portal</param>
        /// <param name="partnerName">Application name or Partner Name</param>
        /// <param name="accountNameForRespectiveSfmcChildAccountAsRegistered">Specify Account Name mentioned for your Sfmc account registration with IRIS (MUCP)</param>
        /// <param name="externalKeyOfTriggeredSendDefnUnderThisAccountName">Specify External Key of your Triggered Send Definition</param>
        public void InitializeIrisApi(string certificateThumprint, long siteId, string partnerName, string accountNameForRespectiveSfmcChildAccountAsRegistered, string externalKeyOfTriggeredSendDefnUnderThisAccountName, string topicId, TimeSpan validity)
        {
            // Try to load the cert from the local machine store, fail if not present.
            var certificate = CertificateHelper.GetCertificateByThumbprint(certificateThumprint, StoreLocation.LocalMachine);
            if (certificate == null)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    "Unable to find client certificate with thumb-print: {0}",
                    certificateThumprint);
                throw new InvalidOperationException(message);
            }

            // Getting a S2S ticket (note that we use MembershipAuthN package)
            s2sAuthClient = S2SAuthClient.Create(siteId, certificate, new Uri(S2SMsaIntAuthenticationEndpoint));

            // Note that the endpoint can be overridden
            publisher = new Publisher(partnerName, certificate, ignoreServerCertificateValidation: true, endpoint: new Uri(MucpEndpoint));

            exactTargetDelivery = new ExactTargetDelivery
            {
                AccountName = accountNameForRespectiveSfmcChildAccountAsRegistered,
                TemplateKey = externalKeyOfTriggeredSendDefnUnderThisAccountName,
                TopicId = topicId,
                Validity = validity
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailRecipients"></param>
        /// <param name="conversationId"></param>
        /// <param name="properties"></param>
        /// <param name="exactTargetDelivery"></param>
        /// <param name="externalKeyOfTriggeredSendDefn"></param>
        /// <returns></returns>
        private async static Task ForwardRequestToIrisApi(EmailRecipients emailRecipients, Guid conversationId, Dictionary<string, string> properties, ExactTargetDelivery exactTargetDelivery, string externalKeyOfTriggeredSendDefn)
        {
            var appTicket = s2sAuthClient.GetAccessTokenAsync(MucpTargetSite, CancellationToken.None).Result;

            await publisher
                .CreateRequest()
                .WithEventName(externalKeyOfTriggeredSendDefn)
                .WithModelProperties(properties)
                .WithInstanceId(conversationId)
                .WithDelivery(exactTargetDelivery)
                .WithRecipient(emailRecipients)
                .WithAppTicket(appTicket)
                .SendAsync(TimeSpan.FromSeconds(120)).ConfigureAwait(false);
        }
        #endregion

        #region TBN_METHODS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalKeyOfTriggeredSendDefnUnderThisAccountName">Specify External Key of your Triggered Send Definition</param>
        public void SendTriggeredRequestToSalesforceThruIrisApi(string externalKeyOfTriggeredSendDefnUnderThisAccountName)
        {
            try
            {
                var emailAddress = "emailaddress001@microsoft.com"; // target email address

                var propertiesExample0 = new Dictionary<string, string>
                {
                     { "FirstName", "SampleFirstName001" },
                     { "LastName", " SampleLastName001" },
                     { "CustomProp001", " Sample CustomProp001" },
                     { "CustomProp002", " Sample CustomProp002" }
                };

                var emailRecipients = new EmailRecipients
                {
                    To = new List<string> { emailAddress }
                };

                var justForExampleNumberOfTriggeredSendRequests = 10;
                List<Task> tasks = new List<Task>();

                for (int idx = 0; idx < justForExampleNumberOfTriggeredSendRequests; idx += 1)
                {
                    // This 'ConversationId' fights Salesforce from sending duplicate emails to end users despite multiple retries by the Tenant or IRIS. 
                    //  So please define a unique identifier per email delivery. Make sure you don’t send same email with different conversation ID otherwise duplicate emails 
                    //  would be send to the user.
                    var conversationId = Guid.NewGuid();

                    // Forwards the requests to IRIS API asychronously
                    tasks.Add(ForwardRequestToIrisApi(emailRecipients, conversationId, propertiesExample0, exactTargetDelivery, externalKeyOfTriggeredSendDefnUnderThisAccountName));

                    Console.Write("\r" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | To go: " + (justForExampleNumberOfTriggeredSendRequests - idx) + "              ");

                    if (idx % 1000 == 0)
                    {
                        try
                        {
                            Task.WaitAll(tasks.ToArray());
                            tasks = new List<Task>();
                        }
                        catch (Exception e)
                        {
                            var message = string.Format(CultureInfo.InvariantCulture, "Request failed with exception: [{0}]", e);
                            Console.WriteLine(message);
                        }
                    }
                }

                Task.WaitAll(tasks.ToArray());

                Console.WriteLine(Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | Completed");
            }
            catch (Exception e)
            {
                var message = string.Format(CultureInfo.InvariantCulture, "Request failed with exception: [{0}]", e);
                Console.WriteLine(message);
            }

            // Forcing logs to be written to 'TextWriterOutput.log'. See trace listener settings in app.config
            Trace.Flush();
        }
        #endregion

        #region BULKSEND_METHODS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flatfile"></param>
        /// <returns></returns>
        public DataTable DisintegrateFlatfileToDatatable(string flatfile, string rowDelimiter, string columnDelimiter)
        {
            var dataTable = new DataTable();
            DataRow dataRow;

            var flatfileContent = File.ReadAllText(flatfile);
            var flatfileLines = Regex.Split(flatfileContent, rowDelimiter);
            var splitFields = Regex.Split(flatfileLines[0], columnDelimiter);
            var numberOfColumns = splitFields.GetLength(0);

            //1st row must be column names; force lower case to ensure matching later on.
            for (int columnIdx = 0; columnIdx < numberOfColumns; columnIdx++)
                dataTable.Columns.Add(splitFields[columnIdx], typeof(string));

            for (int rowIdx = 1; rowIdx < flatfileLines.GetLength(0); rowIdx++)
            {
                splitFields = flatfileLines[rowIdx].Split(new[] { columnDelimiter }, StringSplitOptions.RemoveEmptyEntries);
                dataRow = dataTable.NewRow();
                for (int columnIdx = 0; columnIdx < numberOfColumns; columnIdx++)
                    dataRow[columnIdx] = splitFields[columnIdx];
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable">Data of flatfile</param>
        /// <param name="externalKeyOfTriggeredSendDefnUnderThisAccountName">Specify External Key of your Triggered Send Definition</param>
        public void SendTriggeredRequestsToSalesforceThruIrisApi(DataTable dataTable, string externalKeyOfTriggeredSendDefnUnderThisAccountName)
        {
            try
            {
                var customProperties = new Dictionary<string, string>();
                var emailRecipients = new EmailRecipients();
                List<Task> tasks = new List<Task>();

                var totalCount = dataTable.Rows.Count;
                var currentPosition = 0;
                Console.WriteLine("Total: " + totalCount);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    currentPosition += 1;
                    customProperties.Clear();
                    for (var columnIdx = 0; columnIdx < dataRow.Table.Columns.Count; columnIdx++)
                    {
                        if (dataRow.Table.Columns[columnIdx].ColumnName.Equals("SubscriberKey"))
                            continue;
                        else if (dataRow.Table.Columns[columnIdx].ColumnName.Equals("EmailAddress"))
                            emailRecipients = new EmailRecipients
                            {
                                To = new List<string> { Convert.ToString(dataRow[dataRow.Table.Columns[columnIdx]]) }
                            };
                        else
                            customProperties.Add(dataRow.Table.Columns[columnIdx].ColumnName, Convert.ToString(dataRow[dataRow.Table.Columns[columnIdx]]));
                    }

                    // This 'ConversationId' fights Salesforce from sending duplicate emails to end users despite multiple retries by the Tenant or IRIS. 
                    //  So please define a unique identifier per email delivery. Make sure you don’t send same email with different conversation ID otherwise duplicate emails 
                    //  would be send to the user.
                    var conversationId = Guid.NewGuid();

                    // Forwards the requests to IRIS API asychronously
                    tasks.Add(ForwardRequestToIrisApi(emailRecipients, conversationId, customProperties, exactTargetDelivery, externalKeyOfTriggeredSendDefnUnderThisAccountName));

                    Console.Write("\r" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | To go: " + (totalCount - currentPosition) + "                                 ");

                    if (currentPosition % 1000 == 0)
                    {
                        try
                        {
                            Task.WaitAll(tasks.ToArray());
                            tasks = new List<Task>();
                        }
                        catch (Exception e)
                        {
                            var message = string.Format(CultureInfo.InvariantCulture, "Request failed with exception: [{0}]", e);
                            Console.WriteLine(message);
                        }
                    }
                }

                Task.WaitAll(tasks.ToArray());

                Console.WriteLine(Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | Completed");
            }
            catch (Exception e)
            {
                var message = string.Format(CultureInfo.InvariantCulture, "Request failed with exception: [{0}]", e);
                Console.WriteLine(message);
            }

            // Forcing logs to be written to 'TextWriterOutput.log'. See trace listener settings in app.config
            Trace.Flush();
        }
        #endregion
    }
}
