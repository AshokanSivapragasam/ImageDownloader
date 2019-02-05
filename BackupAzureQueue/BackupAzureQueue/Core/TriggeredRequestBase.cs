using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
using System.Collections.ObjectModel;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// TriggeredRequestBase Class
    /// </summary>
    [DataContract]
    [KnownType(typeof(PartnerTriggerSubscriber))]
    [KnownType(typeof(LimitedProgramSubscriber))]
    [KnownType(typeof(TagmTriggerSubscriber))]
    [KnownType(typeof(GenericSubscriber))]
    public class TriggeredRequestBase : RequestBase
    {
        /// <summary>
        /// Constructor for TriggeredRequestBase Class
        /// </summary>
        public TriggeredRequestBase()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the CommunicationId
        /// </summary>
        [DataMember]
        public int CommunicationId { get; set; }

        /// <summary>
        /// Gets or sets the LimitedProgramId
        /// </summary>
        [DataMember]
        public int LimitedProgramId { get; set; }

        /// <summary>
        /// Gets or sets the DeliveryType
        /// </summary>
        [DataMember]
        public EmailType DeliveryType { get; set; }

        /// <summary>
        /// Gets or sets the TriggerType
        /// </summary>
        [DataMember]
        public TriggeredSendType TriggerType { get; set; }

        /// <summary>
        /// Gets or sets the TriggerDataSource
        /// </summary>
        [DataMember]
        public TriggerDataSource TriggerDataSource { get; set; }

        /// <summary>
        /// Gets or sets the ApplicationName
        /// </summary>
        [DataMember]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the EventDescription
        /// </summary>
        [DataMember]
        public string EventDescription { get; set; }

        /// <summary>
        /// Gets or sets the Subscribers
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), DataMember]
        public List<SubscriberBase> Subscribers { get; set; }

        /// <summary>
        /// Gets or sets the RequestExecutionPriority
        /// </summary>
        [DataMember]
        public RequestExecutionPriorityLevels RequestExecutionPriority { get; set; }

        /// <summary>
        /// Gets or sets the ConversationId
        /// </summary>
        [DataMember]
        public string ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the CustomerKey
        /// </summary>
        [DataMember]
        public string CustomerKey { get; set; }

        /// <summary>
        /// Gets or sets the AccountId
        /// </summary>
        [DataMember]
        public int AccountId { get; set; }
    }
}
