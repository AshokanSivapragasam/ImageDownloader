using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators
{
    /// <summary>
    /// Used in logging to identify what is the error source
    /// </summary>
    public enum ErrorSource
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// EmailInterchange
        /// </summary>
        EmailInterchange = 1,

        /// <summary>
        /// ExactTarget
        /// </summary>
        ExactTarget = 2,

        /// <summary>
        /// MicrosoftInternal
        /// </summary>
        MicrosoftInternal = 3
    }
}
