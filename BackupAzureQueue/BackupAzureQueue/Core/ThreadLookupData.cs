using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// ThreadLookupData Class
    /// </summary>
    public class ThreadLookupData
    {
        /// <summary>
        /// Get or Sets the ExecutionEngineId
        /// </summary>
        public Guid ExecutionEngineId { get; set; }

        /// <summary>
        /// Get or Sets the TransactionType
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Get or Sets the FreeThreadCount
        /// </summary>
        public int FreeThreadCount { get; set; }
    }
}