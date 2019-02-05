using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{

    #region "Email Tracking Summary Details "
    /// <summary>
    /// Email Tracking summary Input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class EmailTrackingInput<T> 
    { 
        /// <summary>
        /// Gets or sets the EmailTrackingCriteria
        /// </summary>
        [DataMember]
        public T EmailTrackingCriteria { get; set; }
    }

    /// <summary>
    /// Email Tracking Summary return type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class EmailTrackingOutput<T>
    {
        /// <summary>
        /// Gets or sets the EmailTrackingResult
        /// </summary>
        [DataMember]
        public T EmailTrackingResult { get; set; }
    }
   
    /// <summary>
    /// Output Type of Email Tracking Summary.
    /// </summary>
      [DataContract]
    public class TriggerSendSummaryDetails 
      {
          /// <summary>
          /// Gets or sets the Bounces
          /// </summary>
          [DataMember]
          public long Bounces { get; set; }

          /// <summary>
          /// Gets or sets the Clicks
          /// </summary>
          [DataMember]
          public long Clicks { get; set; }

          /// <summary>
          /// Gets or sets the NotSentDueToError
          /// </summary>
          [DataMember]
          public long NotSentDueToError { get; set; }

          /// <summary>
          /// Gets or sets the NotSentDueToOptOut
          /// </summary>
          [DataMember]
          public long NotSentDueToOptOut { get; set; }

          /// <summary>
          /// Gets or sets the NotSentDueToUndeliverable
          /// </summary>
          [DataMember]
          public long NotSentDueToUndeliverable { get; set; }

          /// <summary>
          /// Gets or sets the Opens
          /// </summary>
          [DataMember]
          public long Opens { get; set; }

          /// <summary>
          /// Gets or sets the OptOuts
          /// </summary>
          [DataMember]
          public long OptOuts { get; set; }

          /// <summary>
          /// Gets or sets the Sent
          /// </summary>
          [DataMember]
          public long Sent { get; set; }

          /// <summary>
          /// Gets or sets the UniqueClicks
          /// </summary>
          [DataMember]
          public long UniqueClicks { get; set; }

          /// <summary>
          /// Gets or sets the UniqueOpens
          /// </summary>
          [DataMember]
          public long UniqueOpens { get; set; }
      }

    /// <summary>
      /// BulkSendSummaryDetails Class
    /// </summary>
      [DataContract]
      public class BulkSendSummaryDetails
      {
          /// <summary>
          /// Gets or sets the SentDate
          /// </summary>
          [DataMember]
          public DateTime SentDate { get; set; }

          /// <summary>
          /// Gets or sets the NumberSent
          /// </summary>
          [DataMember]
          public long NumberSent { get; set; }

          /// <summary>
          /// Gets or sets the NumberDelivered
          /// </summary>
          [DataMember]
          public long NumberDelivered { get; set; }

          /// <summary>
          /// Gets or sets the NumberErrored
          /// </summary>
          [DataMember]
          public long NumberErrored { get; set; }

          /// <summary>
          /// Gets or sets the NumberExcluded
          /// </summary>
          [DataMember]
          public long NumberExcluded { get; set; }

          /// <summary>
          /// Gets or sets the NumberTargeted
          /// </summary>
          [DataMember]
          public long NumberTargeted { get; set; }

          /// <summary>
          /// Gets or sets the HardBounces
          /// </summary>
          [DataMember]
          public long HardBounces { get; set; }

          /// <summary>
          /// Gets or sets the SoftBounces
          /// </summary>
          [DataMember]
          public long SoftBounces { get; set; }

          /// <summary>
          /// Gets or sets the UniqueClicks
          /// </summary>
          [DataMember]
          public long UniqueClicks { get; set; }

          /// <summary>
          /// Gets or sets the UniqueOpens
          /// </summary>
          [DataMember]
          public long UniqueOpens { get; set; }

          /// <summary>
          /// Gets or sets the Unsubscribes
          /// </summary>
          [DataMember]
          public long Unsubscribes { get; set; }
      }

    /// <summary>
    /// Input type of Email Tracking Summary.
    /// </summary>
    [DataContract]
    public class BulkSendSummaryInput
    {
        /// <summary>
        /// Gets or sets the ClientId
        /// </summary>
        [DataMember]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the TenantBulkSendId
        /// </summary>
        [DataMember]
        public string TenantBulkSendId { get; set; }

        /// <summary>
        /// Gets or sets the TenantBatchID
        /// </summary>
        [DataMember]
        public string TenantBatchID { get; set; }
    }

    /// <summary>
    /// TriggerSendSummaryInput Class
    /// </summary>
    [DataContract]
    public class TriggerSendSummaryInput
    {
        /// <summary>
        /// Gets or sets the ClientId
        /// </summary>
        [DataMember]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the TriggerSendCustomerKey 
        /// </summary>
        [DataMember]
        public string TriggerSendCustomerKey { get; set; }

        /// <summary>
        /// Gets or sets the CommunicationId
        /// </summary>
        [DataMember]
        public string CommunicationId { get; set; }

        /// <summary>
        /// Gets or sets the CommunicationType
        /// </summary>
        [DataMember]
        public string CommunicationType { get; set; }
    }
    #endregion

    #region "Email Tracking Status Details."
    /// <summary>
    /// OutputType of Email Tracking Status.
    /// </summary>
    [DataContract]
    public class EmilTrackingOutputDetails
    {
        /// <summary>
        /// Gets or sets the BounceCategory
        /// </summary>
        [DataMember]
        public string BounceCategory { get; set; }

        /// <summary>
        /// Gets or sets the BounceType
        /// </summary>
        [DataMember]
        public string BounceType { get; set; }

        /// <summary>
        /// Gets or sets the IsBounced
        /// </summary>
        [DataMember]
        public bool IsBounced { get; set; }

        /// <summary>
        /// Gets or sets the IsClicked
        /// </summary>
        [DataMember]
        public bool IsClicked { get; set; }

        /// <summary>
        /// Gets or sets the IsOpened
        /// </summary>
        [DataMember]
        public bool IsOpened { get; set; }

        /// <summary>
        /// Gets or sets the IsSent
        /// </summary>
        [DataMember]
        public bool IsSent { get; set; }

        /// <summary>
        /// Gets or sets the DateBounced
        /// </summary>
        [DataMember]
        public DateTime DateBounced { get; set; }

        /// <summary>
        /// Gets or sets the SentDate
        /// </summary>
        [DataMember]
        public DateTime SentDate { get; set; }

        /// <summary>
        /// Gets or sets the DateOpened
        /// </summary>
        [DataMember]
        public DateTime DateOpened { get; set; }

        /// <summary>
        /// Gets or sets the SMTPCode
        /// </summary>
        [DataMember]
        public string SMTPCode { get; set; }

        /// <summary>
        /// Gets or sets the SMTPReason
        /// </summary>
        [DataMember]
        public string SMTPReason { get; set; }

        /// <summary>
        /// Gets or sets the DateUnSubscribed
        /// </summary>
        [DataMember]
        public DateTime DateUnSubscribed;

        /// <summary>
        /// Gets or sets the IsUnsubscribe
        /// </summary>
        [DataMember]
        public bool IsUnsubscribe { get; set; }

        /// <summary>
        /// Gets or sets the ClickEventTrackingCollection
        /// </summary>
        [DataMember]
        public List<ClickEventTracking> ClickEventTrackingCollection { get; set; }
    }

    /// <summary>
    /// ClickEventTracking Class
    /// </summary>
    [DataContract]
    public class ClickEventTracking 
    {
        /// <summary>
        /// Gets or sets the IsClicked
        /// </summary>
        [DataMember]
        public bool IsClicked { get; set; }

        /// <summary>
        /// Gets or sets the ClickedDate
        /// </summary>
        [DataMember]
        public DateTime ClickedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the URL
        /// </summary>
        [DataMember]
        public string URL { get; set; }
    }

    /// <summary>
    /// Input Type of EmailTracking Status.
    /// </summary>
    [DataContract]
    public class EmailTrackingInputCriteria
    {
        /// <summary>
        /// Gets or sets the TenantTrackingField
        /// </summary>
        [DataMember]
        public string TenantTrackingField { get; set; }

        /// <summary>
        /// Gets or sets the TenantSubscriberKey
        /// </summary>
        [DataMember]
        public string TenantSubscriberKey { get; set; }

        /// <summary>
        /// Gets or sets the ClientID
        /// </summary>
        [DataMember]
        public int ClientID { get; set; }
    }
    #endregion

    #region "DataBase object"
    /// <summary>
    /// Class Contains ET Required fields for performing search operation.
    /// </summary>
    public class BulkSendConfigurationData
    {
        /// <summary>
        /// BulkSendConfigGUID
        /// </summary>
        public int BulkSendConfigurationId { get; set; }

        /// <summary>
        /// Login user ID
        /// </summary>
        public int InterchangeUserId { get; set; }

        /// <summary>
        /// Tenant Account ID  
        /// </summary>
        public int TenantAccountId { get; set; }

        /// <summary>
        /// Tenant import DataExtensions.
        /// </summary>
        public string MasterDEKey { get; set; }

        /// <summary>
        /// ET log after mail has sent to recipients 
        /// </summary>
        public string SendLogDEKey { get; set; }

        /// <summary>
        ///Batchid Field name in ET. 
        /// </summary>
        public string TenantBatchIDField { get; set; }

        /// <summary>
        /// BilkSend Field name in ET.
        /// </summary>
        public string TenantBulkSendField { get; set; }

        /// <summary>
        /// FileFormat
        /// </summary>
        public string FileFormat { get; set; }

        /// <summary>
        /// ImportFileType
        /// </summary>
        public string ImportFileType { get; set; }

        /// <summary>
        /// Tenant Tracking Field Name e.g:OEM
        /// </summary>
        public string TenantTrackingField { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public string DynamicDataExtensionTemplateName { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public string DataImportType { get; set; }       

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public bool IsDynamicDataExtension { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public bool IsSendInvoke { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public string SendableDataExtensionField { get; set; }

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [DataMemberAttribute()]
        public string SendableSubscriberField { get; set; }
    }

    /// <summary>
    /// Class Contains BulkSendClassification Details.
    /// </summary>
    public class BulkSendClassificationData
    {
        /// <summary>
        /// BulkSendClassification
        /// </summary>
        public int BulkSendClassification { get; set; }

        /// <summary>
        /// BulkSendConfigurationId
        /// </summary>
        public int BulkSendConfigurationId { get; set; }

        /// <summary>
        /// TenantAccountId
        /// </summary>
        public int TenantAccountId { get; set; }

        /// <summary>
        /// EmailType
        /// </summary>
        public string EmailType { get; set; }

        /// <summary>
        /// SendClassificationKey
        /// </summary>
        public string SendClassificationKey { get; set; }
    }
    #endregion

    #region "Email tracking Constants"
    /// <summary>
    /// Email Tracking Constants.
    /// </summary>
    public static class EmailTrackingConstants
    {
        /// <summary>
        /// EmailTrackingBulkSummaryDE
        /// </summary>
        public  const string EmailTrackingBulkSummaryDE = "EmailTrackingBulkSummaryDE";

        /// <summary>
        /// EmailTrackingTriggerSummaryDE
        /// </summary>
        public  const string EmailTrackingTriggerSummaryDE = "EmailTrackingTriggerSummaryDE";

        /// <summary>
        /// SentDate
        /// </summary>
        public  const string SentDate = "SentDate";

        /// <summary>
        /// UniqueOpens
        /// </summary>
        public  const string UniqueOpens = "UniqueOpens";

        /// <summary>
        /// NumberSent
        /// </summary>
        public  const string NumberSent = "NumberSent";

        /// <summary>
        /// NumberDelivered
        /// </summary>
        public  const string NumberDelivered = "NumberDelivered";

        /// <summary>
        /// HardBounces
        /// </summary>
        public  const string HardBounces = "HardBounces";

        /// <summary>
        /// SoftBounces
        /// </summary>
        public  const string SoftBounces = "SoftBounces";

        /// <summary>
        /// ID
        /// </summary>
        public  const string ID = "ID";

        /// <summary>
        /// ok
        /// </summary>
        public  const string ok = "ok";

        /// <summary>
        /// BatchGUID
        /// </summary>
        public  const string BatchGUID = "BatchGUID";

        /// <summary>
        /// BulkSendGUID
        /// </summary>
        public  const string BulkSendGUID = "BulkSendGUID";

        /// <summary>
        /// JobID
        /// </summary>
        public  const string JobID = "JobID";

        /// <summary>
        /// CustomerKey
        /// </summary>
        public  const string CustomerKey = "CustomerKey";

        /// <summary>
        /// Sent
        /// </summary>
        public  const string Sent = "Sent";

        /// <summary>
        /// Bounces
        /// </summary>
        public  const string Bounces = "Bounces";

        /// <summary>
        /// Opens
        /// </summary>
        public  const string Opens = "Opens";

        /// <summary>
        /// Clicks
        /// </summary>
        public  const string Clicks = "Clicks";

        /// <summary>
        /// NotSentDueToError
        /// </summary>
        public  const string NotSentDueToError = "NotSentDueToError";

        /// <summary>
        /// NotSentDueToOptOut
        /// </summary>
        public  const string NotSentDueToOptOut = "NotSentDueToOptOut";

        /// <summary>
        /// NotSentDueToUndeliverable
        /// </summary>
        public  const string NotSentDueToUndeliverable = "NotSentDueToUndeliverable";

        /// <summary>
        /// OptOuts
        /// </summary>
        public  const string OptOuts = "OptOuts";

        /// <summary>
        /// UniqueClicks
        /// </summary>
        public  const string UniqueClicks = "UniqueClicks";

        /// <summary>
        /// TirggerSendCustomerKeyConstruction
        /// </summary>
        public const string TirggerSendCustomerKeyConstruction = "{0}_{1}";

        /// <summary>
        /// BounceEvent
        /// </summary>
        public   const string BounceEvent   = "BounceEvent";

        /// <summary>
        /// SendID
        /// </summary>
        public   const string SendID = "SendID";

        /// <summary>
        /// BatchID
        /// </summary>
        public   const string BatchID = "BatchID";

        /// <summary>
        /// SubscriberKey
        /// </summary>
        public   const string SubscriberKey = "SubscriberKey";

        /// <summary>
        /// BounceType
        /// </summary>
        public   const string BounceType = "BounceType";

        /// <summary>
        /// SMTPCode
        /// </summary>
        public   const string SMTPCode = "SMTPCode";

        /// <summary>
        /// SMTPReason
        /// </summary>
        public   const string SMTPReason = "SMTPReason";

        /// <summary>
        /// BounceCategory
        /// </summary>
        public   const string BounceCategory = "BounceCategory";

        /// <summary>
        /// EventDate
        /// </summary>
        public   const string EventDate = "EventDate";

        /// <summary>
        /// EventType
        /// </summary>
        public   const string EventType = "EventType";

        /// <summary>
        /// SentEvent
        /// </summary>
        public   const string SentEvent = "SentEvent";

        /// <summary>
        /// OpenEvent
        /// </summary>
        public   const string OpenEvent = "OpenEvent";

        /// <summary>
        /// ClickEvent
        /// </summary>
        public   const string ClickEvent = "ClickEvent";

        /// <summary>
        /// URL
        /// </summary>
        public   const string URL = "URL";

        /// <summary>
        /// UnSubEvent
        /// </summary>
        public   const string UnSubEvent = "UnSubEvent";

        /// <summary>
        /// SubID
        /// </summary>
        public  const string SubID = "SubID";

        /// <summary>
        /// SubscriberID
        /// </summary>
        public const string SubscriberID = "SubscriberID";

        /// <summary>
        /// EmailAddress
        /// </summary>
        public const string EmailAddress = "EmailAddress";

        /// <summary>
        /// DataFolderPrefix
        /// </summary>
        public const string DataFolderPrefix = "FY ";

        /// <summary>
        /// DynamicDEPrefix
        /// </summary>
        public const string DynamicDEPrefix = "DE_Email";

        /// <summary>
        /// UnableToFindTriggerSendNotFoundException
        /// </summary>
        public const string UnableToFindTriggerSendNotFoundException = "Unable to find Triggered Send of name";
    }
    #endregion

    #region "MSI Constants"
    /// <summary>
    /// MSI Constants.
    /// </summary>
    public static class MSIConstants
    {
        /// <summary>
        /// MSINetTcpProtocol
        /// </summary>
        public const string MSINetTcpProtocol = "sb";

        /// <summary>
        /// MSIHTTPProtocol
        /// </summary>
        public const string MSIHTTPProtocol = "https";

        /// <summary>
        /// MSIHTTPString
        /// </summary>
        public const string MSIHTTPString = "HTTP";

        /// <summary>
        /// MSITCPString
        /// </summary>
        public const string MSITCPString = "TCP";        
    }
    #endregion

}
