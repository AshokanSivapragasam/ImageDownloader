using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// Class EventSubscriber
    /// </summary>
    [DataContract]
    public class EventSubscriber : SubscriberBase
    {
        /// <summary>
        /// Constructor of EventSubscriber Class
        /// </summary>
        public EventSubscriber() : base() { }

        /// <summary>
        /// Gets or sets the LocaleId
        /// </summary>
        [DataMember]
        public int LocaleId { get; set; }

        /// <summary>
        /// Gets or sets the EventId
        /// </summary>
        [DataMember]
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the EmailTypeId
        /// </summary>
        [DataMember]
        public int EmailTypeId { get; set; }

        /// <summary>
        /// Gets or sets the From
        /// </summary>
        [DataMember]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the Subject
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Body
        /// </summary>
        [DataMember]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the CampaignCode
        /// </summary>
        [DataMember]
        public string CampaignCode { get; set; }

        /// <summary>
        /// Gets or sets the ReplyTo
        /// </summary>
        [DataMember]
        public string ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the WweRequestSendDateTime
        /// </summary>
        [DataMember]
        public DateTime WweRequestSendDateTime { get; set; }

        /// <summary>
        /// Gets or sets the TargetAudience
        /// </summary>
        [DataMember]
        public string TargetAudience { get; set; }

        /// <summary>
        /// Gets or sets the TargetProduct
        /// </summary>
        [DataMember]
        public string TargetProduct { get; set; }
    }
}
