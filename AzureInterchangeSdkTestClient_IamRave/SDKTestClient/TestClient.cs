using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureTBNClientSDK = Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.AzureServiceReference;
using System.IO;
using System.Threading;

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
             * Create TBN Generic Against EI API
             */
            /*try { Console.WriteLine(" Create_TBN_Generic_Against_EI_API() -- " + Create_TBN_Generic_Against_EI_API("v-assiva@microsoft.com", "Pass001", "Pass002")); }
            catch (Exception ex) { Console.WriteLine(" Create_TBN_Generic_Against_EI_API() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/

            /*
             * Get request status
             */
            /*try { Console.WriteLine(" GetRequestStatus() -- " + GetRequestStatus()); }
            catch (Exception ex) { Console.WriteLine(" GetRequestStatus() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/

            /*
             * Send bulk-send request
             */
            /*var srcFileName = @"\\HQMSFTSQL03\MS_CLOUD_CAMP\CRMOnlineTtest_2015120315000141.tsv.zip";
            var destFileName = @"\\HQMSFTSQL03\MS_CLOUD_CAMP\CRMOnlineTtest_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".tsv.zip";
            try
            {
                Console.WriteLine(" Counter -- '' BulkSend() -- " + BulkSend(
                    batchId: "2015120615000123",
                    bulkSendId: "2015120615000123",
                    contentId: 230767,
                    filePath: srcFileName,
                    tenantAccountId: "10480840",
                    bulkSendEmailType: BulkSendEmailType.Transactional,
                    isSendInvoke: false,
                    isDynamicDataExtension: true,
                    isOverrideConfiguration: true,
                    dataImportType: DataImportType.Overwrite,
                    dynamicDataExtensionTemplateName: "MSFTv2-Cloud-Data-Schema_v8"));
            }
            catch (Exception ex) { Console.WriteLine(" BulkSend() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/

            var srcFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile.tsv";
            var destFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".tsv";
            File.Copy(srcFileName, destFileName, true);
            try
            {
                Console.WriteLine(" Counter -- '' BulkSend() -- " + BulkSend(
                    batchId: "0",
                    bulkSendId: "1",
                    contentId: 350207,
                    filePath: destFileName,
                    tenantAccountId: "10460681",
                    bulkSendEmailType: BulkSendEmailType.Transactional,
                    isSendInvoke: true,
                    isDynamicDataExtension: false,
                    isOverrideConfiguration: true,
                    dataImportType: DataImportType.AddAndUpdate,
                    dynamicDataExtensionTemplateName: "TriggeredSendDataExtension"));
            }
            catch (Exception ex) { Console.WriteLine(" BulkSend() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

            /*
             * Get tracking summary of bulk-send emails by AccountId, BatchId & BulksendGuid
             */
            /*try { Console.WriteLine(" GetEmailTrackingBulkSendSummaryData() -- " + GetEmailTrackingBulkSendSummaryData()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingBulkSendSummaryData() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }
            /*try { Console.WriteLine(" GetEmailTrackingBulkSendSummaryDataV2() -- " + GetEmailTrackingBulkSendSummaryDataV2()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingBulkSendSummaryDataV2() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/

            /*
             * Get tracking status of emails by AccountId, SubscriberKey & TrackingField
             */
            /*
            try { Console.WriteLine(" GetEmailTrackingStatusData() -- " + GetEmailTrackingStatusData()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingStatusData() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }
            try { Console.WriteLine(" GetEmailTrackingStatusDataV2() -- " + GetEmailTrackingStatusDataV2()); }
            catch (Exception ex) { Console.WriteLine(" GetEmailTrackingStatusDataV2() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }*/

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
        private static string BulkSend(string batchId, string bulkSendId, int contentId, string filePath, string tenantAccountId, BulkSendEmailType bulkSendEmailType, bool isSendInvoke, bool isDynamicDataExtension, bool isOverrideConfiguration, DataImportType dataImportType, string dynamicDataExtensionTemplateName)
        {
            //"b480ee7d-2444-4f37-ba6d-db279b87b60a","b480ee7d-2444-4f37-ba6d-db279b87b60a",@"D:\Usr\ShankarBaradwaj\MslMailerBulkSendList-20150313-255746814.tsv","10290011",BulkSendEmailType.Transactional,true, true,true,DataImportType.AddAndUpdate,"MSL Email Campaigns DE"
            var fileRequest = new FileRequest()
            {
                BatchId = batchId,
                BulkSendId = bulkSendId,
                ContentId = contentId,
                FilePath = filePath,
                TenantAccountId = tenantAccountId,
                BulkSendEmailType = bulkSendEmailType,
                IsSendInvoke = isSendInvoke,
                IsDynamicDataExtension = isDynamicDataExtension,
                IsOverrideConfiguration = isOverrideConfiguration,
                DataImportType = dataImportType,
                DynamicDataExtensionTemplateName = dynamicDataExtensionTemplateName,
                FriendlyFromName = "Gmed"
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
                    ClientId = 39327,
                    //Assign Batch Id.
                    TenantBatchID = "e084362d-6e61-4ef3-9a97-92eaf7365fd2",
                    //Assign Bulk Send Id
                    TenantBulkSendId = "e084362d-6e61-4ef3-9a97-92eaf7365fe3"
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
                    TenantBatchID = "64377d77-d0eb-451b-93a0-93e10953f5ba",
                    //Assign Bulk Send Id
                    TenantBulkSendId = "b480ee7d-2444-4f37-ba6d-db279b87b60d"
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
                    ClientID = 39327,
                    TenantSubscriberKey = "e084362d-6e61-4ef3-9a97-92eaf7365ff1",
                    TenantTrackingField = "e084362d-6e61-4ef3-9a97-92eaf7365fe3"
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
        private static string CallRequestStatus(System.Guid emailInterchangeId, AzureServiceReference.RequestTypeForEnhancedAPI requestType)
        {
            var interchangerequeststatus = new InterchangeRequestStatus();
            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            interchangerequeststatus = client.GetRequestStatus(emailInterchangeId, requestType);
            return string.Format(StringResources.REQUEST_STATUS_MESSAGE, emailInterchangeId + Convert.ToString(interchangerequeststatus.RequestStatusMessage));
        }
        #endregion

        #region TBN_METHODS
        private static string Create_TBN_Generic_Against_EI_API(string emailAddress, string body01, string body02)
        {
            var newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = emailAddress;

            // addition of custom attributes
            newSubscriber.Attributes = new AzureTBNClientSDK.AzureServiceReference.Attribute[2];
            var attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "Param001";
            attribute.Value = body01;
            newSubscriber.Attributes[0] = attribute;

            attribute = new AzureTBNClientSDK.AzureServiceReference.Attribute();
            attribute.Name = "Param002";
            attribute.Value = body02;
            newSubscriber.Attributes[1] = attribute;

            /* Creating a new request*/
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "Generic";
            request.CustomerKey = "AAD_Dashboard_Triggered_Send_001";

            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;

            request.Subscribers = new GenericSubscriber[1];
            // NOTE : the above can be changed to any other type of collection 
            // by configuring the service reference to use 
            // other collection types like List, Collection, ArrayList, etc
            request.Subscribers[0] = newSubscriber;

            EmailInterchangeResult results = EmailInterchangeResult.None;

            /*Instantiating the Proxy Class*/
            AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
            results = client.Send(request);

            return (results.ToString());
        }

        private static bool SendTriggeredWelcomeEmail(string address)
        {
            if (address == null)
            {
                return false;
            }

            // Create the subscriber
            GenericSubscriber newSubscriber = new GenericSubscriber();
            newSubscriber.EmailAddress = address;
            // Create the request
            TriggeredRequestBase request = new TriggeredRequestBase();
            request.ApplicationName = "ResearchNews";
            request.CommunicationId = 12345;
            request.DeliveryType = EmailType.Html;
            request.TriggerDataSource = TriggerDataSource.None;
            request.TriggerType = TriggeredSendType.NoDelay;
            request.Subscribers = new GenericSubscriber[1];
            request.Subscribers[0] = newSubscriber;

            EmailInterchangeResult result = EmailInterchangeResult.None;

            try
            {
                AzureTBNClientSDK.InterchangeConnect client = new AzureTBNClientSDK.InterchangeConnect();
                result = client.Send(request);

                return (result != EmailInterchangeResult.Success) ? false : true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured sending the welcome email. " + ex);
            }

            return false;
        }

        #endregion
    }
}
