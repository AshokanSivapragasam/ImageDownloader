using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureTBNClientSDK = Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.AzureServiceReference;
using System.IO;

namespace Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.TestClient
{
    public class TestClient
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        public static void Main()
        {
            /*
             * Get request status
             */
            try { Console.WriteLine(" GetRequestStatus() -- " + GetRequestStatus()); }
            catch (Exception ex) { Console.WriteLine(" GetRequestStatus() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

            /*
             * Send bulk-send request
             */
            try { Console.WriteLine(" BulkSend() -- " + BulkSend()); }
            catch (Exception ex) { Console.WriteLine(" BulkSend() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

            /*
             * Get tracking summary of bulk-send emails by AccountId, BatchId & BulksendGuid
             */
            try { Console.WriteLine(" GetEmailTrackingBulkSendSummaryData() -- " + GetEmailTrackingBulkSendSummaryData()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingBulkSendSummaryData() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }
            try { Console.WriteLine(" GetEmailTrackingBulkSendSummaryDataV2() -- " + GetEmailTrackingBulkSendSummaryDataV2()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingBulkSendSummaryDataV2() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

            /*
             * Get tracking status of emails by AccountId, SubscriberKey & TrackingField
             */
            try { Console.WriteLine(" GetEmailTrackingStatusData() -- " + GetEmailTrackingStatusData()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingStatusData() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }
            try { Console.WriteLine(" GetEmailTrackingStatusDataV2() -- " + GetEmailTrackingStatusDataV2()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingStatusDataV2() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

            Console.WriteLine("Press any key to close this window");
            Console.ReadLine();
        }

        #region REQUEST_STATUS_METHODS
        /// <summary>
        /// EmailInterchange: Gets status of the request by EiId
        /// </summary>
        /// <returns></returns>
        private static string GetRequestStatus()
        {
            Guid EIID = new Guid("C0250D47-E511-4D4D-A78B-C00BB8815596");
            return CallRequestStatus(EIID, RequestTypeForEnhancedAPI.TBN);
        }
        #endregion

        #region BULKSEND_METHODS
        /// <summary>
        /// Test Method for BulkSend
        /// </summary>
        /// <returns></returns>
        private static string BulkSend()
        {
            var fileRequest = new FileRequest()
            {
                BatchId = "1008",
                BulkSendId = "1",
                ContentId = 312182,
                FilePath = @"D:\Usr\Krishna\EI_HCP_Team_Input_File_003.tsv",
                TenantAccountId = "10460681",
                BulkSendEmailType = BulkSendEmailType.Promotional,
                IsSendInvoke = true,
                IsDynamicDataExtension = true,
                IsOverrideConfiguration = true,
                DataImportType = DataImportType.Overwrite,
                DynamicDataExtensionTemplateName = "ITPROTEMPLATEv2"
            };

            var azureTBNClient = new AzureTBNClientSDK.InterchangeConnect();
            var filesendResult = azureTBNClient.SendFileRequest(fileRequest);
            return (filesendResult.Result + "; EI ID: '" + filesendResult.EmailInterchangeId + "'");
        }
        #endregion

        #region EMAILTRACKING_METHODS
        /// <summary>
        /// EmailTracking: Get BulkSend Summary
        ///     - NumberTargeted
        ///     - NumberDelivered
        ///     - NumberErrored
        ///     - UniqueClicks
        ///     - UniqueOpens
        ///     - Unsubscribes, etc.,
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingBulkSendSummaryData()
        {
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            EmailTrackingOutputOfBulkSendSummaryDetails bulkSummary;

            //Construct Email Tracking Bulk send Summary input object.
            EmailTrackingInputOfBulkSendSummaryInput inputBulkSummary = new EmailTrackingInputOfBulkSendSummaryInput
            {
                EmailTrackingCriteria = new BulkSendSummaryInput
                {
                    //Assign Client ID
                    ClientId = 10460681,
                    //Assign Batch Id.
                    TenantBatchID = "1004",
                    //Assign Bulk Send Id
                    TenantBulkSendId = "1"
                }
            };

            //Retrieve call.
            bulkSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfBulkSendSummaryInput, EmailTrackingOutputOfBulkSendSummaryDetails>(inputBulkSummary);
            Console.WriteLine(bulkSummary.EmailTrackingResult);

            return StringResources.EMAIL_TRACKING_BULK_SEND_SUMMARY_STATUS;
        }

        /// <summary>
        /// EmailTracking: Get BulkSend Summary Version 2
        ///     - NumberTargeted
        ///     - NumberDelivered
        ///     - NumberErrored
        ///     - UniqueClicks
        ///     - UniqueOpens
        ///     - Unsubscribes, etc.,
        ///     - EIRequestId
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingBulkSendSummaryDataV2()
        {
            System.Guid requestID;
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            EmailTrackingOutputOfBulkSendSummaryDetails bulkSummary;
            //Construct Email Tracking Bulk send Summary input object.
            EmailTrackingInputOfBulkSendSummaryInput inputBulkSummary = new EmailTrackingInputOfBulkSendSummaryInput
            {
                EmailTrackingCriteria = new BulkSendSummaryInput
                {
                    //Assign Client ID
                    ClientId = 39327,
                    //Assign Batch Id.
                    TenantBatchID = "6B08CBB1-2B44-4B45-A937-4AE1479C2302",
                    //Assign Bulk Send Id
                    TenantBulkSendId = "6B08CBB1-2B42-4B45-A938-4AE1479C2342"
                }
            };

            //Retrieve call.
            bulkSummary = client.GetEmailTrackingSummaryData<EmailTrackingInputOfBulkSendSummaryInput, EmailTrackingOutputOfBulkSendSummaryDetails>(inputBulkSummary, out requestID);

            return StringResources.EMAIL_TRACKING_BULK_SEND_SUMMARY_STATUS + "EI ID: " + requestID.ToString();
        }

        /// <summary>
        /// EmailTracking: GetStatus
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingStatusData()
        {
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            //EmailTrackingOutput type.
            EmailTrackingOutputOfEmilTrackingOutputDetails emailTracking;
            //Construct EmailTracking Input type.
            EmailTrackingInputOfEmailTrackingInputCriteria objStatusInput = new EmailTrackingInputOfEmailTrackingInputCriteria
            {
                EmailTrackingCriteria = new EmailTrackingInputCriteria
                {
                    ClientID = 10460681,
                    TenantSubscriberKey = "9afcee23-2b03-4a00-b8b0-7ef7188c8379",
                    TenantTrackingField = "9afcee23-2b03-4a00-b8b0-7ef7188c8379"
                }

            };
            //Retrieve call. 
            emailTracking = client.GetEmailTrackingStatusData(objStatusInput);
            return StringResources.EMAIL_TRACKING_STATUS;
        }

        /// <summary>
        /// EmailTracking: GetStatus Version 2
        /// </summary>
        /// <returns></returns>
        private static string GetEmailTrackingStatusDataV2()
        {
            System.Guid requestID;
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();

            //EmailTrackingOutput type.
            EmailTrackingOutputOfEmilTrackingOutputDetails emailTracking;
            //Construct EmailTracking Input type.
            EmailTrackingInputOfEmailTrackingInputCriteria objStatusInput = new EmailTrackingInputOfEmailTrackingInputCriteria
            {
                EmailTrackingCriteria = new EmailTrackingInputCriteria
                {
                    ClientID = 10460681,
                    TenantSubscriberKey = "9afcee23-2b03-4a00-b8b0-7ef7188c8379",
                    TenantTrackingField = "9afcee23-2b03-4a00-b8b0-7ef7188c8379"
                }

            };
            //Retrieve call. 
            emailTracking = client.GetEmailTrackingStatusData(objStatusInput, out requestID);
            return StringResources.EMAIL_TRACKING_STATUS + "EI ID: " + requestID.ToString();
        }
        #endregion

        #region HELPER_METHODS
        private static string CallRequestStatus(System.Guid emailInterchangeId,AzureServiceReference.RequestTypeForEnhancedAPI requestType)
        {
            var interchangerequeststatus = new InterchangeRequestStatus();
            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            interchangerequeststatus = client.GetRequestStatus(emailInterchangeId,requestType);
            return string.Format(StringResources.REQUEST_STATUS_MESSAGE, emailInterchangeId + Convert.ToString(interchangerequeststatus.RequestStatusMessage));
        }
        #endregion
    }
}
