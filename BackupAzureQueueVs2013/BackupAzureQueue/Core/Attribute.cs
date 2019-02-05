using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix"), DataContract]
    public class Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Value { get; set; }
    }
}
