namespace Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Configuration;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using AzureTBNService = Interchange.Platform.Clients.Sdk.AzureServiceReference;
    //using OnPremiseTBNService = Interchange.Platform.Clients.Sdk.OnPremiseTBNServiceReference;
    using System.Reflection;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;


    public sealed class InterchangeConnect
    {
        #region CLASS VARIABLES
        private int retryAttempts;
        private const int UpperRetryLimit = 15;
        private const int LowerRetryLimit = 3;
        private const string InvalidFileExtension = "File Extension is invalid";
        private const string FileSizeException = "uploaded file size cannot be greater than 100 MB";
        private const string FileDoesnotExistsException = "File doesnot exists";
        private const string RequestNullException = "Request can not be null";
        private const string InvalidRequestException = "Request contains invalid values";
        private const string InvalidScheduledDateTimeException = "Request contains invalid ScheduledDateTime";
        private const string ServiceInvalidRequestException = "Invalid values Input Date/FolderId/TenantAccountId";
        private const string ExceptionMessagePredicate = "Request Validation Exception,Reason: ";
        private const string GenericExceptionMessagePredicate = "Cannot process request,Reason: ";
        private const string ErrorExceptionMessagePredicate = "Error Occured while processiong request,Reason: ";
        private const string MSIExceptionMessagePredicate = "Cannot process MSI call,Reason: ";
        private const string EmailRegExPattern = @"^(?:\w|-)+(?:\.(?:\w|-)+)*@(?:(?:\w|-){1,}\.)+(?:\w|-){2,}$";
        private const string PrimaryEndpoint = "PrimaryEndpoint";
        private const string SecondaryEndpoint = "SecondaryEndpoint";
        private const string InvalidRequestId = "RequestId submited is invalid";
        private const string EmptyRequestStatusMessage= "This request id is either invalid or archived. Please contact EIOPS@microsoft.com for further help.";
        private const string Request_Type_Processing_Message="The processing for the {0} request type is currently disabled.";
        private const string Invalid_Request_Type = "The request type provided is not valid.";
        private const string Request_Types_ToBe_Tracked = "RequestTypesToBeTracked";
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor Method that will take care of initialization of RetryAttempts Field.
        /// </summary>
        public InterchangeConnect()
        {
            retryAttempts = 3;
            // Read the value from App.config and try to Cast. In Case Retry Attempts not in the range of 0 to 15. then default should be 3.
            if (Int32.TryParse(ConfigurationManager.AppSettings["RetryAttempts"], out retryAttempts))
            {
                if (retryAttempts <= LowerRetryLimit || retryAttempts > UpperRetryLimit)
                {
                    retryAttempts = 3;
                }
            }
        }
        #endregion

        /// <summary>
        /// Send Method to be called by Tenant.
        /// </summary>
        /// <param name="request">Request that is passed by Tenant.</param>
        /// <returns>Return the Request Acceptance Message to Tenant.</returns>        
        public AzureTBNService.EmailInterchangeResult Send(AzureTBNService.RequestBase request)
        {
            AzureTBNService.EmailInterchangeResponseToken emailInterchangeResponseToken = new AzureTBNService.EmailInterchangeResponseToken();
            emailInterchangeResponseToken = this.SendV2(request);
            return emailInterchangeResponseToken.Result;
        }

        /// <summary>
        /// Request status method to be called by Tenant.
        /// </summary>
        /// <param name="emailInterchangeId">emailInterchangeId that is passed by Tenant.</param>
        /// <returns>Return the User Friendly Message along with the eventid to Tenant.</returns>        
        public AzureTBNService.InterchangeRequestStatus GetRequestStatus(System.Guid emailInterchangeId, AzureTBNService.RequestTypeForEnhancedAPI requestType)
        {
            AzureTBNService.InterchangeRequestStatus interchangeRequestStatus = new AzureTBNService.InterchangeRequestStatus();

             if (string.IsNullOrEmpty(emailInterchangeId.ToString()) || emailInterchangeId == Guid.Empty)
                interchangeRequestStatus.RequestStatusMessage = InvalidRequestId;
             string requestTypesToBeTracked = ConfigurationManager.AppSettings[Request_Types_ToBe_Tracked].ToUpper();
            
            
            try
            {
                if (requestTypesToBeTracked.Trim() != string.Empty)
                {
                    string[] requestTypes = requestTypesToBeTracked.Split(',');

                    if (requestTypes.Contains(requestType.ToString().ToUpper()))
                    {
                        interchangeRequestStatus = CallGetRequestStatus(emailInterchangeId, PrimaryEndpoint, requestType);
                    }
                    else
                    {
                        interchangeRequestStatus.RequestStatusMessage = String.Format(CultureInfo.InvariantCulture, Request_Type_Processing_Message, requestType.ToString().ToUpper());
                    }
                }
                else
                {
                    interchangeRequestStatus.RequestStatusMessage = Invalid_Request_Type;
                }
            }
            catch (Exception)
            {
                interchangeRequestStatus = CallGetRequestStatus(emailInterchangeId, SecondaryEndpoint,requestType);
            }
            
            return interchangeRequestStatus;
        }


        /// <summary>
        /// Send Method to be called by Tenant.
        /// </summary>
        /// <param name="request">Request that is passed by Tenant.</param>
        /// <returns>Return the Request Acceptance Message alogn with the EI IDto Tenant.</returns>        
        public AzureTBNService.EmailInterchangeResponseToken SendV2(AzureTBNService.RequestBase request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(ErrorExceptionMessagePredicate + RequestNullException);
            }

            AzureTBNService.EmailInterchangeResponseToken emailInterchangeResponseToken = new AzureTBNService.EmailInterchangeResponseToken();
            try
            {
                emailInterchangeResponseToken = CallSend(request, PrimaryEndpoint);
            }
            catch (Exception)
            {
                emailInterchangeResponseToken = CallSend(request, SecondaryEndpoint);
            }
            return emailInterchangeResponseToken;
        }


        public AzureTBNService.DataFolderList GetFolderHierarchy(AzureTBNService.RequestForRetrieveFolder folderRequest)
        {
            System.Guid requestID;
            return this.GetFolderHierarchy(folderRequest, out requestID);
        }

        public AzureTBNService.DataFolderList GetFolderHierarchy(AzureTBNService.RequestForRetrieveFolder folderRequest,out System.Guid requestID)
        {
            AzureTBNService.TriggerRequestNotificationClient client = null;
            AzureTBNService.DataFolderList folderList = null;
            int exceptionCount = 0;
            if (folderRequest == null)
            {
                throw new ArgumentNullException(ErrorExceptionMessagePredicate + RequestNullException);
            }

            if (folderRequest.ExactTargetAccountId <= 0)
            {
                throw new Exception("Invalid AccountId.");
            }


            for (int retryCount = 0; retryCount < retryAttempts; retryCount++)
            {
                try
                {
                    using (client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint))
                    {
                        folderList = client.GetFolderHierarchyV2(out requestID, folderRequest);
                    }

                    return folderList;
                }
                catch (Exception ex)
                {
                    if (client == null)
                    {
                        throw ;
                    }

                    exceptionCount++;

                    // Throw exception if maxm retry attempts are exhausted
                    if (exceptionCount == retryAttempts)
                    {
                        throw new Exception(GenericExceptionMessagePredicate + ex.Message.ToString());
                    }
                }

            }
            requestID = Guid.Empty;
            return folderList;
        }

        public AzureTBNService.EmailDataList GetEmailDetails(AzureTBNService.RequestForReturnEmailList requestForReturnEmailList)
        {
            System.Guid requestID;
            return this.GetEmailDetails(requestForReturnEmailList, out requestID);
        }

        public AzureTBNService.EmailDataList GetEmailDetails(AzureTBNService.RequestForReturnEmailList requestForReturnEmailList,out System.Guid requestID)
        {
            if (requestForReturnEmailList == null)
            {
                throw new ArgumentNullException(ErrorExceptionMessagePredicate + RequestNullException);
            }

            if (((DateTime.Compare(requestForReturnEmailList.Date.Date, DateTime.Today.ToUniversalTime()) > 0) || requestForReturnEmailList.Date.Equals(DateTime.MinValue) && requestForReturnEmailList.FolderId <= 0) || requestForReturnEmailList.TenantAccountID <= 0)
            {
                throw new Exception(ErrorExceptionMessagePredicate + ServiceInvalidRequestException);
            }

            AzureTBNService.EmailDataList emailDataList = new AzureTBNService.EmailDataList();            
            emailDataList = CallGetEmailDetails(requestForReturnEmailList, out requestID);

            return emailDataList;
        }

        public AzureTBNService.FileRequestSendResult SendFileRequest(AzureTBNService.FileRequest request)
        {

            string Extension = string.Empty;
            string[] allowedExtensions = { ".tsv", ".csv", ".txt", ".zip", ".gz" };
            long fileSize;
            string breakuptime = string.Empty;
            if (ConfigurationManager.AppSettings["breakupTime"] != null)
            {
                breakuptime = ConfigurationManager.AppSettings["breakupTime"].ToString();
            }
            else
            {
                breakuptime = "12";
            }
            if (request == null)
            {
                throw new Exception(ExceptionMessagePredicate + RequestNullException);
            }
            if (string.IsNullOrEmpty(request.BulkSendId) || request.ContentId < 0 || string.IsNullOrEmpty(request.BatchId) || string.IsNullOrEmpty(request.FilePath))
            {
                throw new Exception(ExceptionMessagePredicate + InvalidRequestException);
            }
            if (request.IsOverrideConfiguration)
            {
                int TenantID = 0;
                int.TryParse(request.TenantAccountId, out TenantID);
                if (string.IsNullOrEmpty(request.TenantAccountId) || request.TenantAccountId.Equals("0") || TenantID < 0)
                {
                    throw new Exception(ExceptionMessagePredicate + InvalidRequestException);
                }
                if (!request.IsSendInvoke.HasValue)
                {
                    request.IsSendInvoke = true;
                }
            }
            if (DateTime.Compare(request.ScheduledDateTime, DateTime.MinValue) != 0)
            {
                if (DateTime.Compare(request.ScheduledDateTime, DateTime.UtcNow.AddHours(Convert.ToInt32(breakuptime))) < 0)
                {
                    throw new Exception(ExceptionMessagePredicate + InvalidScheduledDateTimeException);
                }
            }
            if (!File.Exists(request.FilePath))
            {
                throw new Exception(ExceptionMessagePredicate + FileDoesnotExistsException);
            }
            Extension = Path.GetExtension(request.FilePath);

            if (!allowedExtensions.Contains(Extension))
            {
                throw new Exception(ExceptionMessagePredicate + InvalidFileExtension);
            }


            FileStream fileStream = File.OpenRead(request.FilePath);
            fileSize = fileStream.Length;
            if (fileSize > 100 * 1024 * 1024)
            {
                throw new Exception(ExceptionMessagePredicate + FileSizeException);
            }

            AzureTBNService.FileRequestSendResult fileReqSendResult = new AzureTBNService.FileRequestSendResult();

            fileReqSendResult = CallSendFileRequest(request);

            return fileReqSendResult;
        }
       

        #region"Email Tracking Summary."

        /// <summary>
        /// Function retrives Email Tracking Summary Details.
        /// </summary>
        /// <typeparam name="TInput">EmailTrackingInputOfBulkSendSummaryInput/EmailTrackingInputOfBulkSendSummaryInput</typeparam>
        /// <typeparam name="TOutput">EmailTrackingOutputOfBulkSendSummaryDetails/EmailTrackingOutputOfTriggerSendSummaryDetails</typeparam>
        /// <param name="emailTracking"></param>
        /// <returns></returns>
        public TOutput GetEmailTrackingSummaryData<TInput, TOutput>(TInput emailTracking)
        {
            System.Guid requestID;
            return this.GetEmailTrackingSummaryData<TInput, TOutput>(emailTracking, out requestID);
        }

        /// <summary>
        /// Function retrives Email Tracking Summary Details.
        /// </summary>
        /// <typeparam name="TInput">EmailTrackingInputOfBulkSendSummaryInput/EmailTrackingInputOfBulkSendSummaryInput</typeparam>
        /// <typeparam name="TOutput">EmailTrackingOutputOfBulkSendSummaryDetails/EmailTrackingOutputOfTriggerSendSummaryDetails</typeparam>
        /// <param name="emailTracking"></param>
        /// <returns></returns>
        public TOutput GetEmailTrackingSummaryData<TInput, TOutput>(TInput emailTracking, out System.Guid requestID)
        {
            AzureTBNService.TriggerRequestNotificationClient client = null;
            AzureTBNService.EmailTrackingOutputOfTriggerSendSummaryDetails triggerSummary = null;
            AzureTBNService.EmailTrackingOutputOfBulkSendSummaryDetails bulkSummary = null;
            bool IsBulkSend;
            int exceptionCount = 0;
            object retrunType = null;

            if (emailTracking == null)
                throw new Exception("Null reference exception.");

            using (client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint))
            {
                //Check tenant requesting for Trigger send or Batcg send.
                IsBulkSend = (emailTracking.GetType().IsEquivalentTo(typeof(AzureTBNService.EmailTrackingInputOfBulkSendSummaryInput)) ? true : false);
                //3 retry attempts.
                for (int retryCount = 0; retryCount < retryAttempts; retryCount++)
                {
                    try
                    {
                        if (IsBulkSend)
                        {
                            //Invoke bulk send.
                            bulkSummary = client.GetEmailTrackingBulkSendSummaryDataV2(out requestID, emailTracking as AzureTBNService.EmailTrackingInputOfBulkSendSummaryInput);
                            retrunType = bulkSummary;
                            break;
                        }
                        else
                        {
                            //Invoke Trigger send.
                            triggerSummary = client.GetEmailTrackingTriggerSendSummaryDataV2(out requestID, emailTracking as AzureTBNService.EmailTrackingInputOfTriggerSendSummaryInput);
                            retrunType = triggerSummary;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (client == null)
                        {
                            throw ;
                        }

                        exceptionCount++;

                        // Throw exception if maxm retry attempts are exhausted
                        if (exceptionCount == retryAttempts)
                        {
                            throw new Exception(GenericExceptionMessagePredicate + ex.Message.ToString());
                        }
                    }
                }
            }
            requestID = Guid.Empty;
            return (TOutput)retrunType;
        }

        public AzureTBNService.EmailTrackingOutputOfEmilTrackingOutputDetails GetEmailTrackingStatusData(AzureTBNService.EmailTrackingInputOfEmailTrackingInputCriteria emailTracking)
        {
            System.Guid requestID;
            return this.GetEmailTrackingStatusData(emailTracking, out requestID);
        }

        public AzureTBNService.EmailTrackingOutputOfEmilTrackingOutputDetails GetEmailTrackingStatusData(AzureTBNService.EmailTrackingInputOfEmailTrackingInputCriteria emailTracking, out System.Guid requestID)
        {
            AzureTBNService.TriggerRequestNotificationClient client = null;
            int exceptionCount = 0;
            if (emailTracking == null)
                throw new Exception("Null reference exception.");
            using (client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint))
            {

                for (int retryCount = 0; retryCount < retryAttempts; retryCount++)
                {
                    try
                    {
                        return client.GetEmailTrackingDataV2(out requestID, emailTracking);
                    }
                    catch (Exception ex)
                    {
                        if (client == null)
                        {
                            throw ;
                        }

                        exceptionCount++;

                        // Throw exception if maxm retry attempts are exhausted
                        if (exceptionCount == retryAttempts)
                        {
                            throw new Exception(GenericExceptionMessagePredicate + ex.Message.ToString());
                        }
                    }
                }
            }
            requestID = Guid.Empty;
            return null;
        }

        #endregion

        #region "MSI Methods"

        /// <summary>
        /// Subscribe communications in MS-Individual Using Tool Service
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="communicationId">CommunicationId to Subscribe</param>
        /// <param name="deliveryFormatId">0 = Html , 1 = Text</param>
        /// <returns>Returns the EmailInterchangeResult type</returns>              
        public AzureTBNService.EmailInterchangeResult Subscribe(string email, int communicationId, int deliveryFormatId)
        {
            AzureTBNService.EmailInterchangeResponseToken returnValue = new AzureTBNService.EmailInterchangeResponseToken();
            returnValue = this.SubscribeV2(email, communicationId, deliveryFormatId);
            return returnValue.Result;
        }

        /// <summary>
        /// Subscribe communications in MS-Individual Using Tool Service
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="communicationId">CommunicationId to Subscribe</param>
        /// <param name="deliveryFormatId">0 = Html , 1 = Text</param>
        /// <returns>Returns the EmailInterchangeResult along with the EI ID</returns>              
        public AzureTBNService.EmailInterchangeResponseToken SubscribeV2(string email, int communicationId, int deliveryFormatId)
        {
            AzureTBNService.EmailInterchangeResponseToken emailInterchangeResponseToken = new AzureTBNService.EmailInterchangeResponseToken();
            try
            {
                AzureTBNService.TriggerRequestNotificationClient client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint);

                // Input Validations
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentNullException("Email");
                else if (!Regex.IsMatch(email, EmailRegExPattern))
                    throw new ArgumentException("Invalid Email");

                if (communicationId < 0)
                    throw new ArgumentException("Invalid CommunicationId");

                emailInterchangeResponseToken=client.SubscribeV2(email, communicationId, deliveryFormatId);
            }
            catch (Exception ex)
            {
                throw new Exception(MSIExceptionMessagePredicate + ex.Message.ToString());
            }

            return emailInterchangeResponseToken;
        }


        /// <summary>
        /// Unsubscribe communications in MS-Individual Using Tool Service
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="communicationId">CommunicationId to Unsubscribe</param>
        /// <param name="deliveryFormatId">0 = Html , 1 = Text</param>
        /// <returns>Returns the EmailInterchangeResult type</returns>              
        public AzureTBNService.EmailInterchangeResult Unsubscribe(string email, int communicationId, int deliveryFormatId)
        {
            AzureTBNService.EmailInterchangeResponseToken returnValue = new AzureTBNService.EmailInterchangeResponseToken();
            returnValue = this.UnsubscribeV2(email, communicationId, deliveryFormatId);
            return returnValue.Result;
        }

        /// <summary>
        /// Unsubscribe communications in MS-Individual Using Tool Service
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="communicationId">CommunicationId to Unsubscribe</param>
        /// <param name="deliveryFormatId">0 = Html , 1 = Text</param>
        /// <returns>Returns the EmailInterchangeResult alogn with the EI ID</returns>              
        public AzureTBNService.EmailInterchangeResponseToken UnsubscribeV2(string email, int communicationId, int deliveryFormatId)
        {
            AzureTBNService.EmailInterchangeResponseToken emailInterchangeResponseToken = new AzureTBNService.EmailInterchangeResponseToken();
            try
            {
                AzureTBNService.TriggerRequestNotificationClient client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint);

                // Input Validations
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentNullException("Email");
                else if (!Regex.IsMatch(email, EmailRegExPattern))
                    throw new ArgumentException("Invalid Email");

                if (communicationId < 0)
                    throw new ArgumentException("Invalid CommunicationId");

                emailInterchangeResponseToken=client.UnsubscribeV2(email, communicationId, deliveryFormatId);
            }
            catch (Exception ex)
            {
                throw new Exception(MSIExceptionMessagePredicate + ex.Message.ToString());
            }

            return emailInterchangeResponseToken;
        }

        #endregion

        #region HelperMethods

        /// <summary>
        /// Method for Calling the Azure(Primary/Secondary) Endpoint.
        /// </summary>
        /// <param name="request">Request that is passed by Tenant.</param>
        /// <param name="endpointName">Azure endpoint to be used((Primary/Secondary).</param>
        /// <returns>Returns the Request Acceptance Message along with the EI ID to Calling method.</returns>               
        internal AzureTBNService.EmailInterchangeResponseToken CallSend(AzureTBNService.RequestBase request, string endpointName)
        {
            int exceptionCount = 0;
            AzureTBNService.EmailInterchangeResponseToken emailInterchangeResponseToken = new AzureTBNService.EmailInterchangeResponseToken();
            emailInterchangeResponseToken.Result = AzureTBNService.EmailInterchangeResult.UnknownFailure;

            AzureTBNService.TriggerRequestNotificationClient client = null;
            for (int retryCount = 0; retryCount < retryAttempts; retryCount++)
            {
                try
                {
                    using (client = new AzureTBNService.TriggerRequestNotificationClient(endpointName))
                    {
                        emailInterchangeResponseToken = client.SendV2(request);
                    }

                    if (emailInterchangeResponseToken.Result != AzureTBNService.EmailInterchangeResult.UnknownFailure)
                    {
                        return (emailInterchangeResponseToken);
                    }
                }

                catch (Exception ex)
                {
                    if (client == null)
                    {
                        throw ;
                    }

                    exceptionCount++;

                    // Throw exception if maxm retry attempts are exhausted
                    if (exceptionCount == retryAttempts)
                    {
                        throw new Exception(GenericExceptionMessagePredicate + ex.Message.ToString());
                    }
                }
            }
            return (emailInterchangeResponseToken);
        }
        /// <summary>
        /// Method for Calling the Azure(Primary/Secondary) Endpoint.
        /// </summary>
        /// <param name="requestId">RequestId that is passed by Tenant.</param>
        /// <param name="endpointName">Azure endpoint to be used((Primary/Secondary).</param>
        /// <returns>Returns the user friendly message along with the Event ID to the Calling method.</returns>     
        internal AzureTBNService.InterchangeRequestStatus CallGetRequestStatus(System.Guid emailInterchangeId, string endpointName,AzureTBNService.RequestTypeForEnhancedAPI requestType)
        {
            int exceptionCount = 0;
            AzureTBNService.InterchangeRequestStatus interchangeRequestStatus = new AzureTBNService.InterchangeRequestStatus();
            AzureTBNService.TriggerRequestNotificationClient client = null;

            for (int retryCount = 0; retryCount < retryAttempts; retryCount++)
            {
                try
                {
                    using (client = new AzureTBNService.TriggerRequestNotificationClient(endpointName))
                    {
                        interchangeRequestStatus = client.GetRequestStatus(emailInterchangeId, requestType);
                    }

                   if(string.IsNullOrEmpty(interchangeRequestStatus.RequestStatusMessage))
                        interchangeRequestStatus.RequestStatusMessage = EmptyRequestStatusMessage;
                    break;
                }
                catch (Exception ex)
                {
                    if (client == null)
                    {
                        throw ;
                    }
                    exceptionCount++;

                    // Throw exception if maxm retry attempts are exhausted
                    if (exceptionCount == retryAttempts)
                    {
                        throw new Exception(GenericExceptionMessagePredicate + ex.Message.ToString());
                    }
                }
            }
            return interchangeRequestStatus;
        }

        /// <summary>
        /// Method for Calling the Azure Endpoint.
        /// </summary>        
        /// <param name="request">Request that is passed by Tenant.</param>
        /// <returns>Returns the EmailDataList to Calling method.</returns>
        internal AzureTBNService.EmailDataList CallGetEmailDetails(AzureTBNService.RequestForReturnEmailList request,out Guid requestID)
        {
            int exceptionCount = 0;
            AzureTBNService.EmailDataList azure_EmailDataList = null;
            AzureTBNService.TriggerRequestNotificationClient client = null;
            for (int retryCount = 0; retryCount < retryAttempts; retryCount++)
            {
                try
                {
                    using (client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint))
                    {
                        azure_EmailDataList = client.GetEmailDetailsV2(out requestID,request as AzureTBNService.RequestForReturnEmailList);
                    }
                    return azure_EmailDataList;
                }
                catch (Exception ex)
                {
                    if (client == null)
                    {
                        throw;
                    }

                    exceptionCount++;

                    // Throw exception if maxm retry attempts are exhausted
                    if (exceptionCount == retryAttempts)
                    {
                        throw new Exception(GenericExceptionMessagePredicate + ex.Message.ToString());
                    }
                }
            }
            requestID = Guid.Empty;
            return (azure_EmailDataList);
        }

        /// <summary>
        /// Method for Calling the Azure Endpoint.
        /// </summary>        
        /// <param name="request">Request that is passed by Tenant.</param>
        /// <param name="endpointName">Azure endpoint to be used((Primary/Secondary).</param>
        /// <returns>Returns the FileRequestSendResult to Calling method.</returns>
        internal AzureTBNService.FileRequestSendResult CallSendFileRequest(AzureTBNService.FileRequest request)
        {
            int exceptionCount = 0;
            AzureTBNService.FileRequestSendResult azure_FileRequestSendResult = new AzureTBNService.FileRequestSendResult();
            AzureTBNService.TriggerRequestNotificationClient client = null;
            for (int retryCount = 0; retryCount < retryAttempts; retryCount++)
            {
                try
                {
                    if (FileUpload((request as AzureTBNService.FileRequest).FilePath))
                    {
                        using (client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint))
                        {
                            azure_FileRequestSendResult = client.SendFileRequest(request as AzureTBNService.FileRequest);
                        }
                        return (azure_FileRequestSendResult);
                    }
                }

                catch (Exception ex)
                {
                    if (client == null)
                    {
                        throw;
                    }

                    exceptionCount++;

                    // Throw exception if maxm retry attempts are exhausted
                    if (exceptionCount == retryAttempts)
                    {
                        throw new Exception(GenericExceptionMessagePredicate + ex.Message.ToString());
                    }
                }
            }
            return (azure_FileRequestSendResult);
        }

        private static bool FileUpload(string filePath)
        {
            int bufferSize = (2 * 1024 * 1024);
            int readBuffer = 0;
            byte[] bufferBytes = new byte[bufferSize], tempBuffer;
            DateTime start = DateTime.Now, end = DateTime.Now;
            List<string> blockIds = new List<string>();
            long fileSize = 0;
            string fileName = string.Empty;
            //double elapsed = 0;

            fileName = Path.GetFileName(filePath);
            FileStream fileStream = File.OpenRead(filePath);
            fileSize = fileStream.Length;
            int blockCount = (int)(fileStream.Length / bufferSize) + 1;
            Parallel.For(0, blockCount, i =>
            {
                readBuffer = fileStream.Read(bufferBytes, 0, bufferSize);
                string currentBlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                blockIds.Add(currentBlockId);
                if (readBuffer < bufferSize)
                {
                    tempBuffer = new byte[readBuffer];
                    Array.Copy(bufferBytes, tempBuffer, readBuffer);
                    bufferBytes = tempBuffer;
                }

                using (AzureTBNService.TriggerRequestNotificationClient client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint))
                {
                    client.PutBlock(fileName, currentBlockId, Convert.ToBase64String(bufferBytes));
                }
            });

            using (AzureTBNService.TriggerRequestNotificationClient client = new AzureTBNService.TriggerRequestNotificationClient(PrimaryEndpoint))
            {
                client.PutBlockList(fileName, blockIds.ToArray());
            }

            return true;

        }

        #endregion
    }
}
