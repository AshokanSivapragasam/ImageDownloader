using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// Class EventRequestData
    /// </summary>
    [DataContract]
    [KnownType(typeof(PartnerTriggerSubscriber))]
    [KnownType(typeof(LimitedProgramSubscriber))]
    [KnownType(typeof(TagmTriggerSubscriber))]
    [KnownType(typeof(EventSubscriber))]
    [KnownType(typeof(GenericSubscriber))]
    public class EventRequestData : RequestBase
    {
        /// <summary>
        /// Gets or sets the ApplicationName
        /// </summary>
        [DataMember]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the TemplateId
        /// </summary>
        [DataMember]
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the TriggerType
        /// </summary>
        [DataMember]
        public TriggeredSendType TriggerType { get; set; }

        /// <summary>
        /// Gets or sets the BatchId
        /// </summary>
        [DataMember]
        public Guid BatchId { get; set; }

        /// <summary>
        /// Gets or sets the Subscribers
        /// </summary>
        [DataMember, System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
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
