using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
using System.Collections.ObjectModel;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// Request Class
    /// </summary>
    [Serializable]
    [DataContract]
    public class Request : RequestBase, IRequest
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Request()
        {
            this.StartDateTime = DateTime.UtcNow;
            this.EndDateTime = DateTime.UtcNow;

            this.CurrentRetryAttempt = 0;
        }

        #region Common Properties
        //Common Properties are the properties common across all components (they are readonly for these components)

        /// <summary>
        /// Gets or sets the Request Parameter
        /// </summary>
        [DataMember(IsRequired = false)]
        public string RequestParameter { get; set; }

        /// <summary>
        /// Gets or sets the Request Status (ex:ExternalComplete )
        /// </summary>
        [DataMember(IsRequired = false)]
        public RequestStatus OperationStatus { get; set; }

        /// <summary>
        /// Gets or Sets the ProcessStatus
        /// </summary>
        [DataMember(IsRequired = false)]
        public ProcessStatus ProcessStatus { get; set; }

        #endregion

        #region Shared Properties
        //Shared Proeprties are the properties shared across all/some components (they can be written to and read by various components)

        /// <summary>
        /// Gets or sets the Last Pull DateTime
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime LastPullDateTime { get; set; }

        /// <summary>
        /// Gets or sets the Current Pull Datatime
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime CurrentPullDateTime { get; set; }

        /// <summary>
        /// Gets or Sets the source file corresponding to the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public string SourceFileName { get; set; }

        /// <summary>
        /// Gets or sets the File name which is being processed
        /// </summary>
        [DataMember(IsRequired = false)]
        public string ProcessingFileName { get; set; }

        /// <summary>
        /// Gets or sets the ProcessingFileNameSpec
        /// </summary>
        [DataMember(IsRequired = false)]
        public string ProcessingFileNameSpec { get; set; }

        /// <summary>
        /// Gets or sets the Account Id
        /// </summary>
        [DataMember(IsRequired = false)]
        public string AccountId { get; set; }

        /// <summary>
        /// Get or Sets EnterpriseAccountId associated with the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public int EnterpriseAccountId { get; set; }

        #endregion

        #region Scheulder Properties

        /// <summary>
        /// Gets or sets the Request Scheduler ID
        /// </summary>
        [DataMember(IsRequired = false)]
        public int RequestScheduleId { get; set; }

        /// <summary>
        /// Date when this request would be considered for execution, if it is lesser than current date, request would be picked up immediately
        /// </summary>
        [DataMember(IsRequired = false)]
        public System.DateTime ScheduledDate { get; set; }

        /// <summary>
        /// Gets or sets the ScheduledDateSpecified
        /// </summary>
        [DataMember]
        public System.Boolean ScheduledDateSpecified { get; set; }

        #endregion

        #region DataAdapter Requirements

        /// <summary>
        /// Gets or sets the HasBusinessLogic
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool HasBusinessLogic { get; set; }

        /// <summary>
        /// Gets or sets the Source Name
        /// </summary>
        [DataMember(IsRequired = false)]
        public DataStoreName SourceName { get; set; }

        /// <summary>
        /// Gets or sets the Destination Name
        /// </summary>
        [DataMember(IsRequired = false)]
        public DataStoreName DestinationName { get; set; }

        /// <summary>
        /// Gets or sets the Source Connection Type(ex: SFTP, SQLAPI)
        /// </summary>
        [DataMember(IsRequired = false)]
        public ConnectionType SourceConnectionType { get; set; }

        /// <summary>
        /// Gets or sets the Destination Connection Type
        /// </summary>
        [DataMember(IsRequired = false)]
        public ConnectionType DestinationConnectionType { get; set; }

        /// <summary>
        /// Gets or sets the Connection string of the Destication
        /// </summary>
        [DataMember(IsRequired = false)]
        public string DestinationConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the Source Conncetion String
        /// </summary>
        [DataMember(IsRequired = false)]
        public string SourceConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the Data End points
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DataMember(IsRequired = false)]
        public Collection<DataEndpoint> DataEndpoints { get; set; }

        /// <summary>
        /// Gets or sets the Direction of Request(ex: SourceToDownstream)
        /// </summary>
        [DataMember(IsRequired = false)]
        public RequestDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the Communication Data object
        /// </summary>
        [DataMember(IsRequired = false)]
        public CommunicationData Data { get; set; }

        #endregion

        #region Sftp Component Requirements

        /// <summary>
        /// Determines if the compression is enabled for this request type
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsCompressionEnabled { get; set; }

        /// <summary>
        /// Determines if the decompression is enabled for this request type
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsDecompressionEnabled { get; set; }

        /// <summary>
        /// Determines if the encryption is enabled for this request type
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsEncryptionEnabled { get; set; }

        /// <summary>
        /// Determines if the decryption is enabled for this request type
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsDecryptionEnabled { get; set; }

        #endregion

        /// <summary>
        /// Gets or Sets the EmailInterchangeRequestId
        /// </summary>
        [DataMember]
        public Guid EmailInterchangeRequestId { get; set; }

        /// <summary>
        /// Gets or Sets the ExactTargetRequestId
        /// </summary>
        [DataMember(IsRequired = false)]
        public Guid ExactTargetRequestId { get; set; }

        /// <summary>
        /// Gets or sets the ComponentStates
        /// </summary>
        [DataMember(IsRequired = false)]
        public Dictionary<string, object> ComponentStates { get; private set; }

        /// <summary>
        /// Gets or Sets the RequestType
        /// </summary>
        [DataMember(IsRequired = false)]
        public RequestType RequestType { get; set; }

        /// <summary>
        /// Gets or Sets the TransactionType
        /// </summary>
        [DataMember(IsRequired = false)]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or Sets the IsRequired
        /// </summary>
        [DataMember(IsRequired = false)]
        public ErrorSource RequestError { get; set; }

        /// <summary>
        /// Gets or Sets the DateTime stamp when the request execution was started
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or Sets the DateTime stamp when the request execution was finished
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Gets or Sets the size of the compressed file corresponding to the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public long CompressedFileSize { get; set; }

        /// <summary>
        /// Gets or Sets the size of the encrypted file corresponding to the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public long EncryptedFileSize { get; set; }

        /// <summary>
        /// Gets or Sets the time taken for compressing the file corresponding to the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public int CompressionTime { get; set; }

        /// <summary>
        /// Gets or Sets the time taken for encrypting the file corresponding to the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public int EncryptionTime { get; set; }

        /// <summary>
        /// Gets or Sets the time taken for decompressing the file corresponding to the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public int DecompressionTime { get; set; }

        /// <summary>
        /// Gets or Sets the time taken for decrypting the file corresponding to the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public int DecryptionTime { get; set; }

        /// <summary>
        /// Gets or Sets the Maximum retry attempts of particular request type
        /// </summary>
        [DataMember(IsRequired = false)]
        public Int32 MaxRetryAttempts { get; set; }

        /// <summary>
        /// Gets or Sets the Current retry attempt of particular request
        /// </summary>
        [DataMember(IsRequired = false)]
        public Int32 CurrentRetryAttempt { get; set; }

        /// <summary>
        /// Gets or Sets delay interval of particular request
        /// </summary>
        [DataMember(IsRequired = false)]
        public Int32 DelayBeforeRetry { get; set; }

        /// <summary>
        /// Get or Set Notification Email Address for ET. ET Respond back to EI with this mail address.
        /// </summary>
        [DataMember(IsRequired=false)]
        public string NotificationEmailAddress { get; set; }

        /// <summary>
        /// AddOrUpdateComponentState Method
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddOrUpdateComponentState(string key, object value)
        {
            if (this.ComponentStates == null) this.ComponentStates = new Dictionary<string, object>();
            if (this.ComponentStates.ContainsKey(key))
                this.ComponentStates[key] = value;
            else
                this.ComponentStates.Add(key, value);
        }
    }
}
