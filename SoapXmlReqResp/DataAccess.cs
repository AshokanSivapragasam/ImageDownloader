

namespace Microsoft.IT.RelationshipManagement.Interchange.Platform.Azure.Services.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Runtime.Serialization;
    using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core;
    using RequestEnum = Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
    using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces;
    using Microsoft.WindowsAzure.ServiceRuntime;

    public sealed class DataAccess
    {
        const string InterchangeETProcessorTenant = "InterchangeETProcessor";
        private DataAccess()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="direction"></param>
        /// <param name="soapEnvelope"></param>
        /// <param name="apiAction"></param>
        /// <param name="apiOptions"></param>
        /// <param name="apiObjects"></param>
        /// <param name="apiResults"></param>
        /// <param name="additionalInformation"></param>
        /// <returns></returns>
        static public bool AddSoapRequestResponse(string connectionString, string direction, string soapEnvelope)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int numberOfRecords = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "AddSoapRequestResponse";

                var spDirection = new SqlParameter("@Direction", direction);
                spDirection.DbType = System.Data.DbType.String;

                var spSoapEnvelope = new SqlParameter("@SoapEnvelope", soapEnvelope);
                spSoapEnvelope.DbType = System.Data.DbType.Xml;
                
                command.Parameters.Add(spDirection);
                command.Parameters.Add(spSoapEnvelope);

                command.ExecuteNonQuery();

                return numberOfRecords > 0;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="direction"></param>
        /// <param name="soapEnvelope"></param>
        /// <param name="apiAction"></param>
        /// <param name="apiOptions"></param>
        /// <param name="apiObjects"></param>
        /// <param name="apiResults"></param>
        /// <param name="additionalInformation"></param>
        /// <returns></returns>
        static public bool AddXmlSoapRequestResponse(string connectionString, string apiMethod, string stackMethod, string apiAction, string apiOptions, string apiObjects, string apiResults, string additionalInformation)
        {
            return true;
            SqlConnection connection = null;
            SqlCommand command = null;
            int numberOfRecords = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "AddXmlSoapRequestResponse";
                
                var spApiMethod = new SqlParameter("@ApiMethod", apiMethod);
                spApiMethod.DbType = System.Data.DbType.String;

                var spStackMethod = new SqlParameter("@StackMethod", stackMethod);
                spStackMethod.DbType = System.Data.DbType.String;

                var spApiAction = new SqlParameter("@ApiAction", apiAction);
                spApiAction.DbType = System.Data.DbType.String;

                var spApiOptions = new SqlParameter("@ApiOptions", apiOptions);
                spApiOptions.DbType = System.Data.DbType.Xml;

                var spApiObjects = new SqlParameter("@ApiObjects", apiObjects);
                spApiObjects.DbType = System.Data.DbType.Xml;

                var spApiResults = new SqlParameter("@ApiResults", apiResults);
                spApiResults.DbType = System.Data.DbType.Xml;

                var spAdditionalInformation = new SqlParameter("@AdditionalInformation", additionalInformation);
                spAdditionalInformation.DbType = System.Data.DbType.Xml;
                
                command.Parameters.Add(spApiMethod);
                command.Parameters.Add(spStackMethod);
                command.Parameters.Add(spApiAction);
                command.Parameters.Add(spApiOptions);
                command.Parameters.Add(spApiObjects);
                command.Parameters.Add(spApiResults);
                command.Parameters.Add(spAdditionalInformation);

                command.ExecuteNonQuery();

                return numberOfRecords > 0;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interchangeUserId"></param>
        /// <param name="requestType"></param>
        /// <param name="requestReceiptInfo"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public bool AddRequestReceipt(int interchangeUserId, string requestType, string requestReceiptInfo, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int numberOfRecords = 0;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "AddRequestReceipt";
                
                var spInterchangeUserId = new SqlParameter("@InterchangeUserId", interchangeUserId);
                spInterchangeUserId.DbType = System.Data.DbType.Int32;

                var spRequestType = new SqlParameter("@RequestType", requestType);
                spRequestType.DbType = System.Data.DbType.String;

                var spRequestReceiptInfo = new SqlParameter("@RequestReceiptInfo", requestReceiptInfo);
                spRequestReceiptInfo.DbType = System.Data.DbType.Xml;
                
                command.Parameters.Add(spInterchangeUserId);
                command.Parameters.Add(spRequestType);
                command.Parameters.Add(spRequestReceiptInfo);

                command.ExecuteNonQuery();

                return numberOfRecords > 0;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// Catch exceptions in caller code.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        static public bool CreateRequest<T>(T request, string connectionString, InterchangeUser user = null, string fileSizeInBytes = null)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            string requestType = string.Empty;
            string tenantName = string.Empty;
            string requestExecutionPriority = string.Empty;
            try
            {
                if (null == request)
                    throw new ArgumentNullException("request");
                if (request.GetType() == typeof(InterchangeFileRequest))
                {
                    requestType = request.GetType().Name;
                    if (user != null)
                    {
                        tenantName = user.Alias;
                    }
                }
                else if ((request.GetType() == typeof(Request)) && ((request as Request).RequestType == RequestEnum.RequestType.EmailResponseData
                                                              || (request as Request).RequestType == RequestEnum.RequestType.ReportExtractData))
                {
                    requestType = (request as Request).RequestType.ToString();
                    tenantName = InterchangeETProcessorTenant;
                }
                else if ((request.GetType() == typeof(Request)) && ((request as Request).RequestType == RequestEnum.RequestType.PromotionalListData ||
                                                                    (request as Request).RequestType == RequestEnum.RequestType.SuppressionPromotionalData ||
                                                                    (request as Request).RequestType == RequestEnum.RequestType.SuppressionTransactionalData ||
                                                                    (request as Request).RequestType == RequestEnum.RequestType.CampaignMetaData))
                {
                    requestType = (request as Request).RequestType.ToString();
                    tenantName = user.Alias;
                }
                else
                {
                    requestType = request.GetType().Name;
                    tenantName = (request as TriggeredRequest).ApplicationName;

                    requestExecutionPriority = (request as TriggeredRequest).RequestExecutionPriority.ToString();

                }
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "CreateRequest";

                dynamic requestObj = request;

                SqlParameter RequestID = new SqlParameter("@RequestID", requestObj.EmailInterchangeRequestId);
                RequestID.DbType = System.Data.DbType.Guid;
                SqlParameter objectParam = new SqlParameter("@Object", XmlHelper.DataContractSerialize(request));
                objectParam.DbType = System.Data.DbType.Xml;
                SqlParameter RequestType = new SqlParameter("@RequestType", requestType);
                RequestType.DbType = System.Data.DbType.String;
                SqlParameter TenantName = new SqlParameter("@TenantName", tenantName);
                RequestType.DbType = System.Data.DbType.String;
                SqlParameter RequestExecutionPriority = new SqlParameter("@RequestExecutionPriority", requestExecutionPriority);
                RequestType.DbType = System.Data.DbType.String;
                string dataCenter = string.Empty;
#if DEBUG
                dataCenter = StringResources.DEV_FABRIC;
#else
                dataCenter = RestAPIUtils.GetDataCenterLocation(RoleEnvironment.DeploymentId);
#endif

                SqlParameter DataCenterName = new SqlParameter("@DataCenterName", dataCenter);
                RequestType.DbType = System.Data.DbType.String;
                command.Parameters.Add(RequestID);
                command.Parameters.Add(objectParam);
                command.Parameters.Add(RequestType);
                command.Parameters.Add(TenantName);
                command.Parameters.Add(RequestExecutionPriority);
                command.Parameters.Add(DataCenterName);
                if (!string.IsNullOrWhiteSpace(fileSizeInBytes))
                    command.Parameters.AddWithValue("@FileSizeInBytes", fileSizeInBytes);
                int numberOfRecords = command.ExecuteNonQuery();
                if (numberOfRecords == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }
        
        /// <summary>
        /// Catch exceptions in caller code.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="log"></param> 
        /// <returns></returns>
        static public T ReadRequest<T>(string requestId, string connectionString, RequestEnum.RequestType requestType = RequestEnum.RequestType.None)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "ReadRequest";

                SqlParameter RequestID = new SqlParameter("@RequestID", new Guid(requestId));
                RequestID.DbType = System.Data.DbType.Guid;
                command.Parameters.Add(RequestID);

                SqlParameter RequestType = new SqlParameter("@RequestType", requestType.ToString());
                RequestType.DbType = System.Data.DbType.String;
                command.Parameters.Add(RequestType);

                object serializedObject = command.ExecuteScalar();

                if (serializedObject != null && !String.IsNullOrEmpty(serializedObject.ToString()))
                {
                    T request = (T)XmlHelper.DataContractDeserialize(serializedObject.ToString(), typeof(T), Int32.MaxValue);
                    return request;
                }
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
            return default(T);
        }
        
        /// <summary>
        /// Catch exceptions in caller code.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="log"></param> 
        /// <returns></returns>
        static public bool DeleteRequest(string requestId, string connectionString, bool isDelete = false)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "DeleteRequest";

                SqlParameter RequestID = new SqlParameter("@RequestID", new Guid(requestId));
                RequestID.DbType = System.Data.DbType.Guid;
                SqlParameter IsDelete = new SqlParameter("@isDelete", isDelete);
                IsDelete.DbType = System.Data.DbType.Boolean;
                command.Parameters.Add(RequestID);
                command.Parameters.Add(IsDelete);
                command.ExecuteNonQuery();
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
            return true;
        }

        static public int UpdateRIORequestValues(string requestId, string connectionString, string domain, string operationStatus, string fileSizeInBytes = null)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int isCompleted;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "UpdateRIORequestValues";

                SqlParameter RequestID = new SqlParameter("@RequestID", new Guid(requestId));
                RequestID.DbType = System.Data.DbType.Guid;
                SqlParameter Domain = new SqlParameter("@Domain", domain);
                Domain.DbType = System.Data.DbType.String;
                SqlParameter OperStatus = new SqlParameter("@OperationStatus", operationStatus);
                OperStatus.DbType = System.Data.DbType.String;
                command.Parameters.Add(RequestID);
                command.Parameters.Add(Domain);
                command.Parameters.Add(OperStatus);
                command.Parameters.Add("@IsCompleted", SqlDbType.Int);
                command.Parameters["@IsCompleted"].Direction = ParameterDirection.Output;
                if (!string.IsNullOrWhiteSpace(fileSizeInBytes))
                    command.Parameters.AddWithValue("@FileSizeInBytes", fileSizeInBytes);
                command.ExecuteNonQuery();
                isCompleted = Convert.ToInt32(command.Parameters["@IsCompleted"].Value.ToString());
                return isCompleted;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }

        }

        static public InterchangeUser GetInterchangeUserForThumbprint(string thumbprint, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetInterchangeUserForThumbprint";

                SqlParameter ThumbPrintParam = new SqlParameter("@ThumbPrint", thumbprint);
                command.Parameters.Add(ThumbPrintParam);

                object serializedObject = command.ExecuteScalar();

                if (serializedObject != null && !String.IsNullOrEmpty(serializedObject.ToString()))
                {
                    InterchangeUser interchangeUser = (InterchangeUser)XmlHelper.Deserialize(serializedObject.ToString(), typeof(InterchangeUser));

                    // Decrypt the UserName and Password for the current user
                    interchangeUser.ExactTargetCertificateName = interchangeUser.ExactTargetCertificateName;
                    interchangeUser.ExactTargetPassword = SecretsManager.Decrypt(interchangeUser.ExactTargetPassword);
                    interchangeUser.ExactTargetUsername = SecretsManager.Decrypt(interchangeUser.ExactTargetUsername);
                    interchangeUser.ExactTargetUrl = interchangeUser.ExactTargetUrl;
                    interchangeUser.SFTPUserName = SecretsManager.Decrypt(interchangeUser.SFTPUserName);
                    interchangeUser.SFTPPassword = SecretsManager.Decrypt(interchangeUser.SFTPPassword);
                    interchangeUser.SFTPUrl = interchangeUser.SFTPUrl;
                    interchangeUser.UseETCertificate = interchangeUser.UseETCertificate;
                    interchangeUser.IsMIPEnabled = interchangeUser.IsMIPEnabled;
                    return interchangeUser;
                }
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
            return null;
        }

        /// <summary>
        /// All the Enterprise Accounts details are returned from Database in xml form
        /// </summary>       
        /// <returns>An xml string that contains the details of all the Enterprise Accounts</returns>
        static public DataSet GetEnterpriseAccountUsers(string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            DataSet ds = new DataSet();

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetEnterpriseAccountUsers";

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

                return ds;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }

        static public InterchangeUser GetInterchangeUserForUserId(int userId, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetInterchangeUserForUserId";

                SqlParameter UserIdParam = new SqlParameter("@UserId", userId);
                command.Parameters.Add(UserIdParam);

                object serializedObject = command.ExecuteScalar();

                if (serializedObject != null && !String.IsNullOrEmpty(serializedObject.ToString()))
                {
                    InterchangeUser interchangeUser = (InterchangeUser)XmlHelper.Deserialize(serializedObject.ToString(), typeof(InterchangeUser));
                    interchangeUser.ExactTargetCertificateName = interchangeUser.ExactTargetCertificateName;
                    // Decrypt the UserName and Password for the current user
                    interchangeUser.ExactTargetPassword = SecretsManager.Decrypt(interchangeUser.ExactTargetPassword);
                    interchangeUser.ExactTargetUsername = SecretsManager.Decrypt(interchangeUser.ExactTargetUsername);
                    interchangeUser.ExactTargetUrl = interchangeUser.ExactTargetUrl;
                    interchangeUser.SFTPUserName = SecretsManager.Decrypt(interchangeUser.SFTPUserName);
                    interchangeUser.SFTPPassword = SecretsManager.Decrypt(interchangeUser.SFTPPassword);
                    interchangeUser.SFTPUrl = interchangeUser.SFTPUrl;
                    interchangeUser.UseETCertificate = interchangeUser.UseETCertificate;

                    return interchangeUser;
                }
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
            return null;
        }

        static public RequestClassification GetRequestClassificationByClassificationId(int classificationId, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetRequestClassificationByClassificationId";

                SqlParameter ClassificationIdParam = new SqlParameter("@ClassificationId", classificationId);
                command.Parameters.Add(ClassificationIdParam);

                object serializedObject = command.ExecuteScalar();

                if (serializedObject != null && !String.IsNullOrEmpty(serializedObject.ToString()))
                {
                    RequestClassification requestClassification = (RequestClassification)XmlHelper.Deserialize(serializedObject.ToString(), typeof(RequestClassification));
                    return requestClassification;
                }
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
            return null;

        }

        static public bool MarkRequestAsFailed(Guid requestId, string connectionString, string requestExecutionStatus, bool isFailOverProcessor = false)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "MarkRequestAsFailed";

                SqlParameter requestIdParam = new SqlParameter("@RequestID", requestId);
                requestIdParam.DbType = System.Data.DbType.Guid;
                SqlParameter RequestExecutionStatus = new SqlParameter("@RequestExecutionStatus", requestExecutionStatus);
                RequestExecutionStatus.DbType = System.Data.DbType.String;
                SqlParameter IsFailOverProcessor = new SqlParameter("@IsFailOverProcessor", isFailOverProcessor);
                IsFailOverProcessor.DbType = System.Data.DbType.Boolean;
                command.Parameters.Add(requestIdParam);
                command.Parameters.Add(RequestExecutionStatus);
                command.Parameters.Add(IsFailOverProcessor);
                command.ExecuteNonQuery();

                return true;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }
        static public bool MarkRequestAsReady(Guid requestId, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "MarkRequestAsReady";
                SqlParameter requestIdParam = new SqlParameter("@RequestID", requestId);
                requestIdParam.DbType = System.Data.DbType.Guid;
                command.Parameters.Add(requestIdParam);
                command.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }

        static public bool UpdateFileSize(Guid requestId, string connectionString, string filesizeInBytes)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "UpdateFileSizeForETRequests";
                SqlParameter requestIdParam = new SqlParameter("@EmailInterchangeId", requestId);
                requestIdParam.DbType = System.Data.DbType.Guid;
                SqlParameter fileSizeInBytesParam = new SqlParameter("@FilesizeInBytes", filesizeInBytes);
                fileSizeInBytesParam.DbType = System.Data.DbType.String;
                command.Parameters.Add(requestIdParam);
                command.Parameters.Add(fileSizeInBytesParam);
                command.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
        }


        static public string GetTBNXslFileContent(string xslFileName, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader datareader = null;
            string xslFileContent = string.Empty;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetTBNTypeXslFileContent";
                command.Parameters.Add(new SqlParameter("@XslFilename", xslFileName));
                //SqlParameter filename = new SqlParameter("@XslFilename", xslFileName);
                //filename.DbType = System.Data.DbType.String;
                //command.Parameters.Add(xslFileName);

                Object obj = command.ExecuteScalar();

                if (obj != null && !String.IsNullOrEmpty(obj.ToString()))
                {
                    xslFileContent = obj.ToString();
                }

                return xslFileContent;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (datareader != null) datareader.Close();
                if (connection != null)
                    connection.Close();
            }
        }

        public static IEnumerable<EventAttributes> GetEventAttributes(string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            DataTable data = null;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand("GetEventAttributes", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                data = new DataTable();
                data.Locale = CultureInfo.InvariantCulture;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(data);

                return data.AsEnumerable().Select(c => new EventAttributes(Convert.ToInt32(c["EventID"], CultureInfo.InvariantCulture)
                                                                            , (LogType)Convert.ToInt32(c["EventLogTypeID"], CultureInfo.InvariantCulture)
                                                                            , Convert.ToBoolean(c["LogEvent"], CultureInfo.InvariantCulture)
                                                                            , Convert.ToInt32(c["AzureEventId"], CultureInfo.InvariantCulture)
                                                                            , Convert.ToString(c["SuppressExceptionMessages"], CultureInfo.InvariantCulture)
                                                                            , Convert.ToString(c["UserFriendlyMessage"], CultureInfo.InvariantCulture)));
            }
            finally
            {
                if (data != null)
                {
                    data.Dispose();
                }

                if (adapter != null)
                {
                    adapter.Dispose();
                }

                if (command != null)
                    command.Dispose();

                if (connection != null)
                    connection.Close();
            }
        }

        static public int GetDefaultPriorityForUserId(int interchangeUserId, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int defaultTenantPriority = 0;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetDefaultPriorityForUserId";

                SqlParameter InterchangeUserId = new SqlParameter("@InterchangeUserId", interchangeUserId.ToString());
                InterchangeUserId.DbType = System.Data.DbType.Int16;
                command.Parameters.Add(InterchangeUserId);

                Object obj = command.ExecuteScalar();

                if (obj != null && !String.IsNullOrEmpty(obj.ToString()))
                {
                    Int32.TryParse(obj.ToString(), out defaultTenantPriority);
                }
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
            }
            return (defaultTenantPriority);
        }

        /// <summary>
        /// Get SendLog DataExtension, BatchFieldName and BulkSendFieldName
        /// </summary>
        /// <param name="clinetID"></param>
        /// <param name="userID"></param>
        /// <param name="batchId"></param>
        /// <param name="bulkSendID"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static BulkSendConfigurationData GetBulkSendConfiguration(int interchangeUserID, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            object serializedObject;
            try
            {
                connection = new SqlConnection();
                command = new SqlCommand();

                connection.ConnectionString = connectionString;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@interchangeUserId", interchangeUserID));
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetBulkSendConfiguration";
                connection.Open();
                serializedObject = command.ExecuteScalar();

                if (serializedObject != null && !String.IsNullOrEmpty(serializedObject.ToString()))
                {
                    return (BulkSendConfigurationData)XmlHelper.Deserialize(serializedObject.ToString(), typeof(BulkSendConfigurationData));
                }
            }
            finally
            {
                if (connection != null) connection.Close();
                if (command != null) command.Dispose();
                if (reader != null) reader.Close();
                serializedObject = null;
            }
            return null;
        }

        public static BulkSendClassificationData GetBulkSendClassification(int interchangeUserID, string connectionString, string EmailType)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            object serializedObject;
            try
            {
                connection = new SqlConnection();
                command = new SqlCommand();

                connection.ConnectionString = connectionString;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@interchangeUserId", interchangeUserID));
                command.Parameters.Add(new SqlParameter("@emailType", EmailType));
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetBulkSendClassification";
                connection.Open();
                serializedObject = command.ExecuteScalar();

                if (serializedObject != null && !String.IsNullOrEmpty(serializedObject.ToString()))
                {
                    return (BulkSendClassificationData)XmlHelper.Deserialize(serializedObject.ToString(), typeof(BulkSendClassificationData));
                }
            }
            finally
            {
                if (connection != null) connection.Close();
                if (command != null) command.Dispose();
                if (reader != null) reader.Close();
                serializedObject = null;
            }
            return null;
        }

        public static void DBConnectVerification(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static Dictionary<string, List<string>> GetEnterpriseAccountsToBePolled(string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            Dictionary<string, List<string>> enterpriseAccounts = new Dictionary<string, List<string>>();

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetEnterpriseAccountsToBePolled";
                    connection.Open();
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        List<string> EnterpriseAccountDetails = new List<string>();
                        EnterpriseAccountDetails.Add(reader["ExactTargetCertificateName"].ToString());
                        EnterpriseAccountDetails.Add(reader["UseETCertificate"].ToString());
                        EnterpriseAccountDetails.Add(reader["ExactTargetURL"].ToString());
                        EnterpriseAccountDetails.Add(SecretsManager.Decrypt(reader["WebClientUserName"].ToString()));
                        EnterpriseAccountDetails.Add(SecretsManager.Decrypt(reader["WebClientPassword"].ToString()));
                        EnterpriseAccountDetails.Add(SecretsManager.Decrypt(reader["SFTPClientUserName"].ToString()));
                        EnterpriseAccountDetails.Add(SecretsManager.Decrypt(reader["SFTPClientPassword"].ToString()));
                        EnterpriseAccountDetails.Add(reader["ExactTargetSFTPURL"].ToString());
                        enterpriseAccounts.Add(reader["EnterpriseAccountId"].ToString(), EnterpriseAccountDetails);
                    }
                }


                return enterpriseAccounts;
            }
            finally
            {
                if (command != null) command.Dispose();
                if (reader != null) reader.Close();
            }
        }

        /// <summary>
        ///  Lists all the request id's to be processed(isProcessingd=0 and isFailedState=0) based on the request type
        /// </summary>
        /// <param name="requestType">Request type</param>
        /// <param name="connectionString">Database connection string</param>
        /// <returns></returns>
        public static List<string> GetRequestIdsToBeProcessed(RequestEnum.RequestType requestType, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            List<string> requestIds = new List<string>();
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetRequestIdsToBeProcessed";

                SqlParameter RequestType = new SqlParameter("@RequestType", requestType.ToString());
                RequestType.DbType = System.Data.DbType.String;

                command.Parameters.Add(RequestType);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    requestIds.Add(reader["RequestId"].ToString());
                }
                return requestIds;
            }
            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null)
                    connection.Close();
                if (reader != null) reader.Close();
            }
        }


        /// <summary>
        ///  Get User Friendly exception message details from the Events table
        /// </summary>
        /// <param name="emailInterchangeId">Request id submitted by Tenant</param>
        /// <param name="connectionString">To connect to interchange DB</param>
        /// <returns> InterchangeRequestStatus Class</returns>
        public static InterchangeRequestStatus GetRequestStatusMessage(Guid emailInterchangeId, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int eventid;

            InterchangeRequestStatus interchangerequeststatus = new InterchangeRequestStatus();
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetUserMessageforRequestId";
                // Adding input parameter
                SqlParameter RequestId = new SqlParameter("@RequestId", emailInterchangeId);
                RequestId.DbType = System.Data.DbType.Guid;
                command.Parameters.Add(RequestId);
                // Adding output parameters
                command.Parameters.Add(new SqlParameter("@EventId", SqlDbType.Int));
                command.Parameters["@EventId"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();

                interchangerequeststatus.Properties = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(command.Parameters["@EventId"].Value.ToString()))
                    interchangerequeststatus.Properties.Add("EventId", "0");

                else
                {
                    interchangerequeststatus.Properties.Add("EventId", Convert.ToString(command.Parameters["@EventId"].Value));
                    eventid = Convert.ToInt32(command.Parameters["@EventId"].Value.ToString());

                    if (Log.EventAttributes != null)
                    {
                        var eventAttribute = Log.EventAttributes.SingleOrDefault(c => c.EventId == eventid);
                        interchangerequeststatus.RequestStatusMessage = eventAttribute.UserFriendlyMessage;
                    }
                }
            }

            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return interchangerequeststatus;
        }

        public static string GetFileSizeForFallBackRequests(Guid emailInterchangeId, string connectionString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            string size = string.Empty;


            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetFileSizeForFallBackRequests";
                // Adding input parameter
                SqlParameter RequestId = new SqlParameter("@RequestId", emailInterchangeId);
                RequestId.DbType = System.Data.DbType.Guid;
                command.Parameters.Add(RequestId);
                // Adding output parameters
                command.Parameters.Add(new SqlParameter("@FileSizeInBytes", SqlDbType.NVarChar, 5000));
                command.Parameters["@FileSizeInBytes"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                size = command.Parameters["@FileSizeInBytes"].Value.ToString();
            }

            finally
            {
                if (null != command)
                    command.Dispose();
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return size;
        }
    }

    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class InterchangeUser
    {
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public Int32 InterchangeUserId { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String Alias { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String CertificateThumbprint { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public Boolean ApiAccessEnabled { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public Boolean TriggeredRequestEnabled { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public Int32 ClassificationId { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String AccountId { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String ExactTargetCertificateName { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String ExactTargetPassword { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String ExactTargetUsername { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String ExactTargetUrl { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String ValidTriggeredSendTypes { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String SFTPUserName { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String SFTPPassword { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public String SFTPUrl { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public Boolean UseETCertificate { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public Boolean IsMIPEnabled { get; set; }
    }

    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class RequestClassification
    {
        /// <summary>
        /// ClassificationId.
        /// </summary>
        [DataMemberAttribute()]
        public Int32 ClassificationId { get; set; }

        /// <summary>
        /// Request Priority.
        /// </summary>
        [DataMemberAttribute()]
        public Int32 Priority { get; set; }

        /// <summary>
        /// PriorityName.
        /// </summary>
        [DataMemberAttribute()]
        public String PriorityName { get; set; }

        /// <summary>
        /// ThreadLatencyInMilliSeconds.
        /// </summary>
        [DataMemberAttribute()]
        public Int32 ThreadLatencyInMilliSeconds { get; set; }

        /// <summary>
        /// ThreadProcessingPower.
        /// </summary>
        [DataMemberAttribute()]
        public Int32 ThreadProcessingPower { get; set; }

        /// <summary>
        /// QueueInvisibleTimeInMilliSeconds.
        /// </summary>
        [DataMemberAttribute()]
        public Int32 QueueInvisibleTimeInMilliSeconds { get; set; }
    }


}
