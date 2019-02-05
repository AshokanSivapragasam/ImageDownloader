using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;
using System.Runtime.Serialization;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// FileRequestSendResult Class
    /// </summary>
    public class FileRequestSendResult
    {
        /// <summary>
        /// Gets or sets the Result
        /// </summary>
        [DataMember]
        public EmailInterchangeResult Result;

        /// <summary>
        /// Gets or sets the EmailInterchangeId
        /// </summary>
        [DataMember]
        public Guid EmailInterchangeId { get; set; }

    }
}
