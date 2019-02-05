using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// SubscriberBase Class
    /// </summary>
    [DataContract]
    [KnownType(typeof(PartnerTriggerSubscriber))]
    [KnownType(typeof(LimitedProgramSubscriber))]
    [KnownType(typeof(TagmTriggerSubscriber))]
    [KnownType(typeof(GenericSubscriber))]
    [KnownType(typeof(EventSubscriber))]
    public abstract class SubscriberBase
    {
        /// <summary>
        /// Get or Sets the SubscriberKey
        /// </summary>
        [DataMember]
        public String SubscriberKey { get; set; }

        /// <summary>
        /// Get or Sets the EmailAddress
        /// </summary>
        [DataMember]
        public String EmailAddress { get; set; }

        /// <summary>
        /// Get or Sets the Attributes
        /// </summary>
        [DataMember, System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public Collection<Attribute> Attributes { get; set; }

        /// <summary>
        /// Constructor for SubscriberBase Class
        /// </summary>
        protected SubscriberBase()
        {
        }
    }
}
