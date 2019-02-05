using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// Defines the common metadata associated with each request, that remains constant for each request type
    /// </summary>
    public struct RequestTypeMeta
    {
        /// <summary>
        /// Gets or Sets the type of the request
        /// </summary>
        public RequestType RequestType { get; set; }

        /// <summary>
        /// Gets or Sets the maximum retry attempts to execute the specified request type
        /// </summary>
        public int MaximumRetryAttempts { get; set; }

        /// <summary>
        /// Gets or Sets the delay in minutes before which execution of a request type should be restarted from point of failure
        /// </summary>
        public int DelayBeforeRetry { get; set; }

        /// <summary>
        /// Gets or Sets the TransactionType associated with each request type
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or sets whether compression is enabled for this request type
        /// </summary>
        public bool IsCompressionEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether decompression is enabled for this request type
        /// </summary>
        public bool IsDecompressionEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether encryption is enabled for this request type
        /// </summary>
        public bool IsEncryptionEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether decryption is enabled for this request type
        /// </summary>
        public bool IsDecryptionEnabled { get; set; }

        ///// <summary>
        ///// Compares whether a specified object is equal to the instance or not
        ///// </summary>
        ///// <param name="obj">Specifies the object with which the instance must be compared</param>
        ///// <returns>Returns a value indicating whether equality was detected</returns>
        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}
    }
}
