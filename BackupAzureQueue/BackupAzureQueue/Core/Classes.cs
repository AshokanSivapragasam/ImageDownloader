using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces;


namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// RequestTypeSourceMapEntry
    /// </summary>
    public class RequestTypeSourceMapEntry
    {

        #region Automatic Properties
        /// <summary>
        /// Gets or sets the Collection of source types
        /// </summary>
        public Collection<DataStoreName> DataStoreNames
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the Request Type, ex:WizardData,SubscriptionData
        /// </summary>
        public RequestType RequestType
        {
            get;
            private set;
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="types">data store type</param>
        /// <param name="typeOfRequest">Request Type</param>
        public RequestTypeSourceMapEntry(Collection<DataStoreName> types, RequestType typeOfRequest)
        {
            DataStoreNames = types;
            RequestType = typeOfRequest;
        }
    }
    
    /// <summary>
    /// RequestPacket Class
    /// </summary>    
    [Serializable]
    [KnownType(typeof(Request))]
    [KnownType(typeof(TriggeredRequest))]
    public class RequestPacket 
    {
        #region Fields/Automatic Properties
        /// <summary>
        /// Readonly Exact Target RequestId
        /// </summary>        
        [DataMember(IsRequired = false)]
        public Guid ExactTargetRequestId { get; private set; }
        
        /// <summary>
        /// Read only Email Interchange Request Id
        /// </summary>
       
        [DataMember(IsRequired = false)]
        public Guid EmailInterchangeRequestId { get; private set; }
        
        /// <summary>
        /// Gets or sets the readonly Request Type 
        /// </summary>
        
        [DataMember(IsRequired = false)]
        public RequestType RequestType { get; private set; }

        /// <summary>
        /// Gets or sets the Request Object
        /// </summary>        
        [DataMember(IsRequired = false)]
        public IRequest Request { get; set; }

        /// <summary>
        /// readonly Transaction Type (long or short)
        /// </summary>        
        [DataMember(IsRequired = false)]
        public TransactionType TransactionType { get; private set; }
        
        /// <summary>
        /// Gets or sets the Process Status (Ex: EIReceiveSuccess)
        /// </summary>
        
        [DataMember(IsRequired = false)]
        public ProcessStatus ProcessStatus { get; set; }
        #endregion;

       
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emailInterchangeRequestId">EI Request ID</param>
        /// <param name="exactTargetRequestId">ET Request ID</param>
        /// <param name="type">Request Type</param>
        /// <param name="newRequest">Request Object</param>
        /// <param name="processStatus">Process Status</param>
        /// <param name="transType">Transaction Type</param>
        public RequestPacket(Guid emailInterchangeRequestId, Guid exactTargetRequestId, RequestType type, IRequest newRequest, ProcessStatus processStatus, TransactionType transType)
        {
            this.ExactTargetRequestId = exactTargetRequestId;            
            this.EmailInterchangeRequestId = emailInterchangeRequestId;
            this.RequestType = type;
            this.Request = newRequest;
            this.TransactionType = transType;
            this.ProcessStatus = processStatus;
        }
    }

    // Using List have performance over the Collection object, we are very much sure that we will be working with 
    // the RequestList object and dont require any extensibility w.r.t RequestList
    /// <summary>
    /// Class which holds list of Request objects
    /// </summary>
    [DataContract]
    [Serializable]
    public class RequestList 
    {
        /// <summary>
        /// Gets or sets the Request Object
        /// </summary>
        [DataMember(IsRequired = true)]
        public Request Request { get; set; }
    }

    /// <summary>
    /// Request Schedule information holder class to be used by Scheduler component only
    /// </summary>
    [DataContract]
    [Serializable]
    public class RequestSchedule
    {
        /// <summary>
        /// Gets or sets the Request Schedule Id
        /// </summary>
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Request object list
        /// </summary>
        [DataMember(IsRequired = true)]
        public RequestList RequestData { get; set; }
    }

    /// <summary>
    /// Request Schedule information holder class to be used by Scheduler component only
    /// </summary>
    [CollectionDataContract]
    [Serializable]
    public class RequestScheduleCollection : List<RequestSchedule>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DataEndpoint
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataEndpoint()
        {
            FilterList = new Collection<FilterDefinition>();
            ColumnList = new Collection<ColumnDefinition>();
        }

        /// <summary>
        /// Gets or sets the Collection of Filters
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DataMember(IsRequired = false)]
        public Collection<FilterDefinition> FilterList { get; set; }

        /// <summary>
        /// Gets or sets the Collection of Columns
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DataMember(IsRequired = false)]
        public Collection<ColumnDefinition> ColumnList { get; set; }

        /// <summary>
        /// Gets or sets the DataEndPoint ID
        /// </summary>
        [DataMember(IsRequired = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the DataEndPoint Name
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Destination Object Name
        /// </summary>
        [DataMember(IsRequired = false)]
        public string DestinationObjectName { get; set; }

        /// <summary>
        /// Gets or sets the Source Object Name
        /// </summary>
        [DataMember(IsRequired = false)]
        public string SourceObjectName { get; set; }

        /// <summary>
        /// Determines whether deleta data will be provided or not
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsDeltaProvided { get; set; }

        /// <summary>
        /// Determines whether deleta data will be required or not
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsDeltaRequired { get; set; }

        /// <summary>
        /// Determines whether File is required or not
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsFileRequired { get; set; }

    }

    /// <summary>
    /// Column Definition Class, defines mapping between source and destination columns
    /// </summary>
    [Serializable]
    public class ColumnDefinition
    {
        /// <summary>
        /// Constructor for ColumnDefinition Class
        /// </summary>
        public ColumnDefinition()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceColumnName">Source Column Name</param>
        /// <param name="destinationColumnName">Destination Column Name</param>
        public ColumnDefinition(string sourceColumnName, string destinationColumnName)
        {
            SourceColumnName = sourceColumnName;
            DestinationColumnName = destinationColumnName;
        }

        #region Automatic Properties
        /// <summary>
        /// Gets or sets the Source Column Name
        /// </summary>
        public string SourceColumnName
        {
            get;
            set;

        }
        /// <summary>
        /// Gets or sets the Destination Column Name
        /// </summary>
        public string DestinationColumnName
        {
            get;
            set;
        }
        #endregion
    }

    /// <summary>
    /// Filter Definition Class
    /// </summary>
    [Serializable]
    public class FilterDefinition
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FilterDefinition()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Filter Name</param>
        /// <param name="value">Filter Value</param>
        /// <param name="filterType">Filter Type</param>
        public FilterDefinition(string name, string value, FilterType filterType)
        {
            Name = name;
            Value = value;
            FilterType = filterType;
        }

        #region Automatic Properties
        /// <summary>
        /// Gets or sets the Filter Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Filter Value
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Filter Type
        /// </summary>
        public FilterType FilterType
        {
            get;
            set;
        }
        #endregion
    }

    /// <summary>
    /// Defines the Class for Message Queue body
    /// </summary>
    [Serializable]
    public class InterchangeMessage
    {
        #region Automatic Properties
        
        /// <summary>
        /// Gets or sets the ET Request Id
        /// </summary>
        public Guid ExactTargetRequestId
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the IsService
        /// </summary>
        public bool IsService
        {
            get;
            set;
        }
        
        /// <summary>
        /// Message + Inner exception Message from the exception object
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Source from the exception object
        /// </summary>        
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Stack Trace from the exception object
        /// </summary>
        public string StackData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Entity information Class.Method
        /// </summary>
        public string Entity
        {
            get;
            set;
        }
        /// <summary>
        /// To get/set the machine name from which the request has arrived
        /// </summary>
        public string MachineName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Event ID in case of exception
        /// </summary>
        public int EventId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Time of event occurunce
        /// </summary>
        public DateTime DateAndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Identifier of the object in this case request
        /// </summary>
        public Guid EmailInterchangeRequestId
        {
            get;
            set;
        }

        /// <summary>
        /// Severity 0 - Information, Severity 1 - Warning, Severity 2 - Exception
        /// </summary>
        public SeverityType Severity
        {
            get;
            set;
        }

        /// <summary>
        /// MessageLevel 1 - High, 2 - Medium, 3 - Low
        /// </summary>
        public MessageLevel MessageLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Send notification or not
        /// </summary>
        public bool SendNotification
        {
            get;
            set;
        }

        /// <summary>
        /// Type of object to fill if object provided.
        /// </summary>
        public ObjectIdentifier ObjectType
        {
            get;
            set;
        }

        /// <summary>
        /// Type of object to fill if object provided.
        /// </summary>
        public string Object
        {
            get;
            set;
        }

        #endregion
    }
    /// <summary>
    /// Defines the class for ThreadLookup table
    /// </summary>
    public class ThreadManagementLookupEntry
    {
        #region Automatic Properties
        /// <summary>
        /// Gets or sets the ExecutionEngineId
        /// </summary>
        public Guid ExecutionEngineId
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the ThreadTransactionType
        /// </summary>
        public TransactionType ThreadTransactionType
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the ThreadPriority
        /// </summary>
        public System.Threading.ThreadPriority ThreadPriority
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the MaxThreadCount
        /// </summary>
        public int MaxThreadCount
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the QueuePath
        /// </summary>
        public String QueuePath
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="priority"></param>
        /// <param name="threadCount"></param>
        /// <param name="path"></param>
        public ThreadManagementLookupEntry(Guid id, TransactionType type, System.Threading.ThreadPriority priority, int threadCount, String path)
        {
            ExecutionEngineId = id;
            ThreadTransactionType = type;
            ThreadPriority = priority;
            MaxThreadCount= threadCount;
            QueuePath = path;
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class CurrentThreadManagementEntry
    {
       #region Automatic Properties

        /// <summary>
        /// Request Transaction Type
        /// </summary>
        public TransactionType TransactionType
        {
            get;
            private set;
        }
        /// <summary>
        /// Execution Engine/Control Engine Id
        /// </summary>
        public Guid ExecutionEngineId
        {
            get;
            private set;
        }
        /// <summary>
        /// Current Thread count of EE or CE
        /// </summary>
        public int CurrentThreadCount
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Execution Engine ID</param>
        /// <param name="type">Request Transaction Type</param>
        /// <param name="currentCount">Current Thread Count</param>
        public CurrentThreadManagementEntry(Guid id, TransactionType type, int currentCount)
        {
            ExecutionEngineId = id;
            TransactionType = type;
             CurrentThreadCount= currentCount;
        }
        #endregion
    }

    /// <summary>
    /// Will specify the details of the resource to be managed
    /// </summary>
    public class ResourceDetails
    {
        /// <summary>
        /// Defines Management service type(ex: ThreadManagement)
        /// </summary>
        public ResourceManagementServiceType ResourceType
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies which caller EE or CE
        /// </summary>
        public Guid CallerIdentifier
        {
            get;
            set;
        }
        
        /// <summary>
        /// Transaction Type
        /// </summary>
        public TransactionType ResourceTransactType
        {
            get;
            set;
        }
        
        /// <summary>
        /// Type of the Request
        /// </summary>
        public RequestType TypeOfRequest
        {
            get;
            set;
        }
        
        /// <summary>
        /// Operation which is performed on the Resouce (ex:LockResource)
        /// </summary>
        public ResourceOperation ResourceActivity
        {
            get;
            set;
        }

        /// <summary>
        /// Data store name(Ex: Geneva)
        /// </summary>
        public DataStoreName StoreName
        {
            get;
            set;
        }
    }


    /// <summary>
    /// CommunicationData Class
    /// </summary>
    [Serializable]
    [DataContract]
    public class CommunicationData 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommunicationData()
        {
            AudienceXml = null;
            CommunicationRegularNewsletterId = null;
            ConfirmationMessageCulture = null;
            ConfirmationMessageTitle = null;
        }

        /// <summary>
        /// Gets or Sets whether the request correspond to a new communication or not
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool IsNewCommunication { get; set; }

        /// <summary>
        /// Gets or Sets whether the request is to be hidden on the profile center UI
        /// </summary>
        [DataMember(IsRequired = false)]
        public int HideFromProfileCenter { get; set; }
        
        /// <summary>
        /// Gets or Sets the Communication ID corresponding to this request
        /// </summary>
        [DataMember]
        public int CommunicationId { get; set; }

        /// <summary>
        /// Gets or sets the CustomObjectKey
        /// </summary>
        [DataMember]
        public string CustomObjectKey { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the TypeId
        /// </summary>
        [DataMember]
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the ClassTitle
        /// </summary>
        [DataMember]
        public string ClassTitle { get; set; }
        
        /// <summary>
        /// Gets or sets the ClassDescription
        /// </summary>
        [DataMember]
        public string ClassDescription { get; set; }

        /// <summary>
        /// Gets or sets the ClassId
        /// </summary>
        [DataMember]
        public int ClassId { get; set; }

        /// <summary>
        /// Gets or sets the LanguageIsoCode3
        /// </summary>
        [DataMember]
        public string LanguageIsoCode3 { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmationMessageTitle
        /// </summary>
        [DataMember]
        public string ConfirmationMessageTitle { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmationMessageCulture
        /// </summary>
        [DataMember]
        public string ConfirmationMessageCulture { get; set; }

        /// <summary>
        /// Gets or sets the Country Code
        /// </summary>
        [DataMember]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the LCID
        /// </summary>
        [DataMember]
        public string LCID { get; set; }

        /// <summary>
        /// Gets or sets the OwnerAlias
        /// </summary>
        [DataMember]
        public string OwnerAlias { get; set; }

        /// <summary>
        /// Gets or sets the ActiveInd
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ind"), DataMember]
        public int ActiveInd { get; set; }

        /// <summary>
        /// Gets or sets the CustomerDiscoverable
        /// </summary>
        [DataMember]
        public int CustomerDiscoverable { get; set; }

        /// <summary>
        /// Gets or sets the CustomerCanUnsubscribeInd
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ind"), DataMember]
        public int CustomerCanUnsubscribeInd { get; set; }

        /// <summary>
        /// Gets or sets the HiddenInd
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ind"), DataMember]
        public int HiddenInd { get; set; }
        
        /// <summary>
        /// Gets or sets the AudienceXml
        /// </summary>
        [DataMember]
        public string AudienceXml { get; set; }

        /// <summary>
        /// Gets or sets the CommunicationRegularNewsletterId
        /// </summary>
        [DataMember]
        public int? CommunicationRegularNewsletterId { get; set; }

        /// <summary>
        /// Gets or sets the CommunicationRegularNewsletterId
        /// </summary>
        [DataMember]
        public string CommunicationAttributes { get; set; }
    }
}
