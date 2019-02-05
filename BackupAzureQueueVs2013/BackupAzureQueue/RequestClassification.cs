namespace Microsoft.IT.RelationshipManagement.Interchange.Platform.Azure.Services.Common
{

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;


    [DataContract]
    public class BaseQueueMessage
    {
        public object Context { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Tenant { get; set; }

        [DataMember]
        public int TenantId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string RequestType { get; set; }
    }

    [DataContract]
    public class RequestMessageClassificationCritical : BaseQueueMessage
    {
    }

    [DataContract]
    public class RequestMessageClassificationHigh : BaseQueueMessage
    {
    }

    [DataContract]
    public class RequestMessageClassificationMedium : BaseQueueMessage
    {
    }

    [DataContract]
    public class RequestMessageClassificationLow : BaseQueueMessage
    {

    }

    [DataContract]
    public class FileRequestMessageClassification : BaseQueueMessage
    {
        [DataMember]
        public bool isFallbackProcessing { get; set; }

        [DataMember]
        public int numberOfAttemptsForCreatingBulksendDefinition { get; set; }
    }

    [DataContract]
    public class InterchangeETResponseMessage : BaseQueueMessage
    {
        [DataMember]
        public string AccountId { get; set; }

        [DataMember]
        public Guid ExactTargetId { get; set; }

        [DataMember]
        public string SourceFileName { get; set; }

        [DataMember]
        public string DestinationFileName { get; set; }

        [DataMember]
        public bool isFailed { get; set; }

        [DataMember]
        public bool isFallback { get; set; }
    }

    [DataContract]
    public class FileFailedRequestMessage : BaseQueueMessage
    {
    }

    [DataContract]
    public class InterchangeETQueueMessage : BaseQueueMessage
    {

    }

    [DataContract]
    public class InterchangeRIOBaseQueueMessage : BaseQueueMessage
    {
        [DataMember]
        public string RequestProcessingStatus { get; set; }

        [DataMember]
        public string RequestProcessingFileName { get; set; }

        [DataMember]
        public int EnterpriseAccountId { get; set; }

        [DataMember]
        public string SubsidiaryAccountId { get; set; }
    }

    [DataContract]
    public class InterchangeRIOQueueMessage : InterchangeRIOBaseQueueMessage
    {
    }

    [DataContract]
    public class InterchangeRIOProcessedQueueMessage : InterchangeRIOBaseQueueMessage
    {
    }

    [DataContract]
    public class FailedRequestMessage : BaseQueueMessage
    {
        public FailedRequestMessage(BaseQueueMessage message)
        {
            if (null == message)
                throw new ArgumentNullException("message");

            this.Id = message.Id;
            this.Tenant = message.Tenant;
            this.TenantId = message.TenantId;
            this.RequestType = message.RequestType;
        }

        [DataMember]
        public int numberOfAttemptsForCreatingBulksendDefinition { get; set; }
    }

    public enum RequestClassificationType
    {
        Critical = 0,
        High = 1,
        Medium = 2,
        Low = 3,
    }
    public enum RequestProcessingStatus
    {

        ReadyToProcess = 0,

        InProgress_OnPremise = 1,

        Completed_OnPremise = 2,

        Failed_OnPremise = 3,

    }

}
