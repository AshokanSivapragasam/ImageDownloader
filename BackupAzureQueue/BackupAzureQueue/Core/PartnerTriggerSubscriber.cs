using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// PartnerTriggerSubscriber Class
    /// </summary>
    [DataContract]
    public class PartnerTriggerSubscriber : SubscriberBase
    {
        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        [DataMember]
        public String FirstName { get; set; }

        /// <summary>
        /// Gets or sets the MiddleName
        /// </summary>
        [DataMember]
        public String MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the LastName1
        /// </summary>
        [DataMember]
        public String LastName1 { get; set; }

        /// <summary>
        /// Gets or sets the LastName2
        /// </summary>
        [DataMember]
        public String LastName2 { get; set; }

        /// <summary>
        /// Gets or sets the NamePrefix
        /// </summary>
        [DataMember]
        public String NamePrefix { get; set; }

        /// <summary>
        /// Gets or sets the NameSuffix
        /// </summary>
        [DataMember]
        public String NameSuffix { get; set; }

        /// <summary>
        /// Gets or sets the CountryCode
        /// </summary>
        [DataMember]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the RegistrationDate
        /// </summary>
        [DataMember]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the PartnerAttributes 
        /// </summary>
        [DataMember]
        public string PartnerAttributes { get; set; }
        
        /// <summary>
        /// Constructor for PartnerTriggerSubscriber Class
        /// </summary>
        public PartnerTriggerSubscriber()
            : base()
        {

        }
    }
}