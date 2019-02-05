namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators
{
    /// <summary>
    /// Enum ResourceOperation
    /// </summary>
    public enum ResourceOperation
    {
        /// <summary>
        /// LockResource
        /// </summary>
        LockResource,

        /// <summary>
        /// FreeResource
        /// </summary>
        FreeResource
    }

    /// <summary>
    /// Enum ActivityStatus
    /// </summary>
    public enum ActivityStatus
    {
        /// <summary>
        /// Retry
        /// </summary>
        Retry,

        /// <summary>
        /// Terminate
        /// </summary>
        Terminate,

        /// <summary>
        /// Completed
        /// </summary>
        Completed
    }
    /// <summary>
    /// 
    /// </summary>
    public enum ResourceManagementServiceType
    {
        /// <summary>
        /// ThreadManagement
        /// </summary>
        ThreadManagement,
        /// <summary>
        /// SessionManagement
        /// </summary>
        SessionManagement
    }

    /// <summary>
    /// ObjectIdentifier
    /// </summary>
    public enum ObjectIdentifier
    {
        /// <summary>
        /// Request
        /// </summary>
        Request = 0, 

        /// <summary>
        /// RequestPacket
        /// </summary>
        RequestPacket=1
    }

    /// <summary>
    /// Enum RequestStatus
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>
        /// ExternalNew
        /// </summary>
        ExternalNew = 0,

        /// <summary>
        /// ExternalReceived
        /// </summary>
        ExternalReceived = 1,

        /// <summary>
        /// ExternalComplete
        /// </summary>
        ExternalComplete = 2,

        /// <summary>
        /// ExternalError
        /// </summary>
        ExternalError = 3,

        /// <summary>
        /// Complete
        /// </summary>
        Complete = 4,

        /// <summary>
        /// CorpRequestCleanup
        /// </summary>
        CorpRequestCleanup = 5,

        /// <summary>
        /// EdgeRequestCleanup
        /// </summary>
        EdgeRequestCleanup = 6,

        /// <summary>
        /// ExternalRetry
        /// </summary>
        ExternalRetry = 7,

        /// <summary>
        /// New
        /// </summary>
        New = 8
    }

    /// <summary>
    /// Enum ComponentType
    /// </summary>
    public enum ComponentType
    {
        /// <summary>
        /// ExecutionEngine
        /// </summary>
        ExecutionEngine = 0,

        /// <summary>
        /// RequestDispatcher
        /// </summary>
        RequestDispatcher,

        /// <summary>
        /// RequestAssembler
        /// </summary>
        RequestAssembler,

        /// <summary>
        /// Scheduler
        /// </summary>
        Scheduler,

        /// <summary>
        /// PollingEngine
        /// </summary>
        PollingEngine,

        /// <summary>
        /// FileNotificationService
        /// </summary>
        FileNotificationService,

        /// <summary>
        /// ETAdapter
        /// </summary>
        ETAdapter,

        /// <summary>
        /// SftpClient
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sftp")]
        SftpClient,

        /// <summary>
        /// LogHandler
        /// </summary>
        LogHandler,

        /// <summary>
        /// LogWriter
        /// </summary>
        LogWriter,

        /// <summary>
        /// DataAdapter
        /// </summary>
        DataAdapter,

        /// <summary>
        /// FileManager
        /// </summary>
        FileManager,

        /// <summary>
        /// RequestStore
        /// </summary>
        RequestStore
    }

    /// <summary>
    /// Enum DataStoreName
    /// </summary>
    public enum DataStoreName
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// Geneva
        /// </summary>
        Geneva,

        /// <summary>
        /// ExactTarget
        /// </summary>
        ExactTarget,

        /// <summary>
        /// MSC
        /// </summary>
        MSC,

        /// <summary>
        /// Genesis
        /// </summary>
        Genesis,

        /// <summary>
        /// MSI
        /// </summary>
        MSI,

        /// <summary>
        /// Suppression
        /// </summary>
        Suppression,

        /// <summary>
        /// GenevaBridgeApi
        /// </summary>
        GenevaBridgeApi
    }   


    /// <summary>
    /// Enum RequestDirection
    /// </summary>
    public enum RequestDirection
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// SourceToDownstream
        /// </summary>
        SourceToDownstream,

        /// <summary>
        /// DownstreamToSource
        /// </summary>
        DownstreamToSource
    }

    /// <summary>
    /// Enum MessageLevel
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum MessageLevel
    {
        /// <summary>
        /// High
        /// </summary>
        High = 1, 
        
        /// <summary>
        /// Medium
        /// </summary>
        Medium = 2, 
        
        /// <summary>
        /// Low
        /// </summary>
        Low = 3
    }

    /// <summary>
    /// FilterType
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// EqualsFilter
        /// </summary>
        EqualsFilter,

        /// <summary>
        /// InFilter
        /// </summary>
        InFilter
    }

    /// <summary>
    /// Enum SeverityType
    /// </summary>
    public enum SeverityType
    {
        /// <summary>
        /// Information
        /// </summary>
        Information = 0, 
        
        /// <summary>
        /// Warning
        /// </summary>
        Warning = 1, 
        
        /// <summary>
        /// Exception
        /// </summary>
        Exception = 2
    }

    /// <summary>
    /// Enum ObjectType
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// ETObject
        /// </summary>
        ETObject = 0, 
        
        /// <summary>
        /// EIObject
        /// </summary>
        EIObject = 1
    }

    /// <summary>
    /// Enum ConnectionType
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// SqlApi
        /// </summary>
        SqlApi,

        /// <summary>
        /// FlatFile
        /// </summary>
        FlatFile,

        /// <summary>
        /// WebService
        /// </summary>
        WebService,

        /// <summary>
        /// Sftp
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sftp")]
        Sftp
    }

    /// <summary>
    /// Use by EE, CE, RD, RA
    /// </summary>
    public enum ProcessStatus
    {
        /// <summary>
        /// RequestAssemble
        /// </summary>
        RequestAssemble,

        /// <summary>
        /// RequestPush
        /// </summary>
        RequestPush,

        /// <summary>
        /// EIReceiveSuccess
        /// </summary>
        EIReceiveSuccess,

        /// <summary>
        /// EIReceiveFailure
        /// </summary>
        EIReceiveFailure,

        /// <summary>
        /// ETUpdateSuccess
        /// </summary>
        ETUpdateSuccess,

        /// <summary>
        /// ETUpdateFailure
        /// </summary>
        ETUpdateFailure,

        /// <summary>
        /// CorpnetRequestCleanup
        /// </summary>
        CorpnetRequestCleanup,

        /// <summary>
        /// PhoenixRequestCleanup
        /// </summary>
        PhoenixRequestCleanup,

        /// <summary>
        /// ArchiveData
        /// </summary>
        ArchiveData,

        /// <summary>
        /// FileUploadSucess
        /// </summary>
        FileUploadSucess,

        /// <summary>
        /// BulkSendSuccess
        /// </summary>
        BulkSendSuccess,

        /// <summary>
        /// ExternalError
        /// </summary>
        ExternalError
    }

    /// <summary>
    /// Enum FileTriggerStatuses
    /// </summary>
    public enum FileTriggerStatuses
    {
        /// <summary>
        /// ExternalComplete
        /// </summary>
        ExternalComplete,

        /// <summary>
        /// Complete
        /// </summary>
        Complete,

        /// <summary>
        /// ErrorOnTasks
        /// </summary>
        ErrorOnTasks,

        /// <summary>
        /// Error
        /// </summary>
        Error
    }

    /// <summary>
    /// Enum Process
    /// </summary>
    public enum Process
    {
        /// <summary>
        /// ETUpdateEngine
        /// </summary>
        ETUpdateEngine,

        /// <summary>
        /// EIReceiveEngine
        /// </summary>
        EIReceiveEngine
    }

    /// <summary>
    /// Enum RequestExecutionStatus
    /// </summary>
    public enum RequestExecutionStatus
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Started
        /// </summary>
        Started = 1,

        /// <summary>
        /// InProgress
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Completed 
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Failed
        /// </summary>
        Failed = 4,

        /// <summary>
        /// BetweenRetries
        /// </summary>
        BetweenRetries = 5
    }

    /// <summary>
    /// AzureRequestExecutionStatus
    /// </summary>
    public enum AzureRequestExecutionStatus
    {
        /// <summary>
        /// Completed
        /// </summary>
        Completed = 0,

        /// <summary>
        /// Warning
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Failed
        /// </summary>
        Failed = 2
    }

    /// <summary>
    /// Specifies where all the message would get logged
    /// </summary>
    public enum LogDestination
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Database
        /// </summary>
        Database = 10,

        /// <summary>
        /// EventViewer
        /// </summary>
        EventViewer = 20,

        /// <summary>
        /// LogEverywhere
        /// </summary>
        LogEverywhere=50
    }
}
