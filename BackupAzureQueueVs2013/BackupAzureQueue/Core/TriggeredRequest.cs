using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// TriggeredRequest Class
    /// </summary>
    [DataContract]
    [KnownType(typeof(PartnerTriggerSubscriber))]
    [KnownType(typeof(LimitedProgramSubscriber))]
    [KnownType(typeof(TagmTriggerSubscriber))]
    [KnownType(typeof(GenericSubscriber))]
    [KnownType(typeof(EventRequestData))]
    public class TriggeredRequest : RequestBase, IRequest
    {
       // EventRequestData eventsRequestReference = null;
        /// <summary>
        /// Constructor for TriggeredRequest Class
        /// </summary>
        /// <param name="requestBase"></param>
        public TriggeredRequest(RequestBase requestBase)
            : base()
        {
            this.EmailInterchangeRequestId = Guid.NewGuid();
            this.StartDateTime = DateTime.UtcNow;
            this.EndDateTime = DateTime.UtcNow;

            this.CurrentRetryAttempt = 0;
            this.MaxRetryAttempts = 6;

            if (requestBase != null)
            {
                TriggeredRequestBase triggeredRequestReference = requestBase as TriggeredRequestBase;
                EventRequestData eventsRequestReference = requestBase as EventRequestData;
               
                if (triggeredRequestReference!= null)
                {
                    this.CommunicationId = triggeredRequestReference.CommunicationId;
                    this.LimitedProgramId = triggeredRequestReference.LimitedProgramId;
                    this.ApplicationName = triggeredRequestReference.ApplicationName;
                    this.DeliveryType = triggeredRequestReference.DeliveryType;
                    this.TriggerDataSource = triggeredRequestReference.TriggerDataSource;
                    this.TriggerType = triggeredRequestReference.TriggerType;
                    this.EventDescription = triggeredRequestReference.EventDescription;
                    this.Subscribers = triggeredRequestReference.Subscribers;
                    this.RequestExecutionPriority = triggeredRequestReference.RequestExecutionPriority;
                    if (String.IsNullOrWhiteSpace(triggeredRequestReference.Subscribers[0].SubscriberKey))
                        triggeredRequestReference.Subscribers[0].SubscriberKey = Guid.NewGuid().ToString();

                    this.ConversationId = String.IsNullOrEmpty(triggeredRequestReference.ConversationId) ? triggeredRequestReference.Subscribers[0].SubscriberKey : triggeredRequestReference.ConversationId;
                    this.CustomerKey = triggeredRequestReference.CustomerKey;
                    this.AccountId = Convert.ToString(triggeredRequestReference.AccountId);
                }
                else if (eventsRequestReference!= null)
                {
                    this.ApplicationName = eventsRequestReference.ApplicationName;
                    this.TriggerType = eventsRequestReference.TriggerType;
                    this.Subscribers = eventsRequestReference.Subscribers;
                    this.BatchId = eventsRequestReference.BatchId;
                    this.TemplateId = eventsRequestReference.TemplateId;
                    this.RequestExecutionPriority = eventsRequestReference.RequestExecutionPriority;
                    this.ConversationId = String.IsNullOrEmpty(eventsRequestReference.ConversationId) ? eventsRequestReference.BatchId.ToString() : eventsRequestReference.ConversationId;
                    this.CustomerKey = eventsRequestReference.CustomerKey;
                    this.AccountId = Convert.ToString(eventsRequestReference.AccountId);
                }
            }
        }


        #region IRequest Members
        /// <summary>
        /// Get or Set the EmailInterchangeRequestId
        /// </summary>
        [DataMember]
        public Guid EmailInterchangeRequestId { get; private set; }

        /// <summary>
        /// Get or Set the ExactTargetRequestId
        /// </summary>
        [DataMember(IsRequired = false)]
        public Guid ExactTargetRequestId { get; set; }

        /// <summary>
        /// Get or Set the ComponentStates
        /// </summary>
        [DataMember(IsRequired = false)]
        public Dictionary<string, object> ComponentStates { get; private set; }

        /// <summary>
        /// Get or Set the RequestType
        /// </summary>
        [DataMember(IsRequired = false)]
        public RequestType RequestType { get; set; }

        /// <summary>
        /// Get or Set the TransactionType
        /// </summary>
        [DataMember(IsRequired = false)]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Get or Set the RequestError
        /// </summary>
        [DataMember(IsRequired = false)]
        public ErrorSource RequestError { get; set; }

        /// <summary>
        /// Get or Set the OperationStatus
        /// </summary>
        [DataMember(IsRequired = false)]
        public RequestStatus OperationStatus { get; set; }

        /// <summary>
        /// Get or Set the ProcessStatus
        /// </summary>
        [DataMember(IsRequired = false)]
        public ProcessStatus ProcessStatus { get; set; }

        /// <summary>
        /// Get or Set the MaxRetryAttempts
        /// </summary>
        [DataMember(IsRequired = false)]
        public Int32 MaxRetryAttempts { get; set; }

        /// <summary>
        /// Get or Set the CurrentRetryAttempt
        /// </summary>
        [DataMember(IsRequired = false)]
        public Int32 CurrentRetryAttempt { get; set; }

        /// <summary>
        /// Get or Set the DelayBeforeRetry
        /// </summary>
        [DataMember(IsRequired = false)]
        public Int32 DelayBeforeRetry { get; set; }

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

        #endregion

        /// <summary>
        /// Get or Set the ApplicationName
        /// </summary>
        [DataMember]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Get or Set the TriggerType
        /// </summary>
        [DataMember]
        public TriggeredSendType TriggerType { get; set; }

        /// <summary>
        /// Get or Set the Subscribers
        /// </summary>
        [DataMember, System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<SubscriberBase> Subscribers { get; set; }

        #region TriggeredRequestBase Members

        /// <summary>
        /// Get or Set the CustomerKey
        /// </summary>
        [DataMember]
        public string CustomerKey { get; set; }

        /// <summary>
        /// Get or Set the CommunicationId
        /// </summary>
        [DataMember]
        public int CommunicationId { get; set; }

        /// <summary>
        /// Get or Set the LimitedProgramId
        /// </summary>
        [DataMember]
        public int LimitedProgramId { get; set; }

        /// <summary>
        /// Get or Set the DeliveryType
        /// </summary>
        [DataMember]
        public EmailType DeliveryType { get; set; }

        /// <summary>
        /// Get or Set the TriggerDataSource
        /// </summary>
        [DataMember]
        public TriggerDataSource TriggerDataSource { get; set; }

        /// <summary>
        /// Get or Set the EventDescription
        /// </summary>
        [DataMember]
        public string EventDescription { get; set; }

        #endregion

        #region BatchRequestData Members
        /// <summary>
        /// Get or Sets the BatchId
        /// </summary>
        [DataMember]
        public Guid BatchId { get; set; }

        /// <summary>
        /// Gets or Sets the TemplateId
        /// </summary>
        [DataMember]
        public string TemplateId { get; set; }
       
        #endregion

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
        /// Date when this request would be considered for execution, if it is lesser than current date, request would be picked up immediately
        /// </summary>
        [DataMember(IsRequired = false)]
        public System.DateTime ScheduledDate { get; set; }

        /// <summary>
        /// Get or Sets Subsidiary AccountId associated with the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public string AccountId { get; set; }
        /// <summary>
        /// Get or Sets EnterpriseAccountId associated with the request
        /// </summary>
        [DataMember(IsRequired = false)]
        public int EnterpriseAccountId { get; set; }

        /// <summary>
        /// Get or Set the RequestExecutionPriority 
        /// </summary>
        [DataMember]
        public RequestExecutionPriorityLevels RequestExecutionPriority { get; set; }

        /// <summary>
        /// Get or Set the ConversationId 
        /// </summary>
        [DataMember]
        public string ConversationId { get; set; }
    }
}