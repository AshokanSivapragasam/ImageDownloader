using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// Output Type of Enhanced EI API response.
    /// </summary>
    [DataContract]
    public class InterchangeRequestStatus
    {
        /// <summary>
        /// Gets or sets the RequestStatusMessage
        /// </summary>
        [DataMember]
        public string RequestStatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the Properties
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Properties { get; set; }

    }
  
}
