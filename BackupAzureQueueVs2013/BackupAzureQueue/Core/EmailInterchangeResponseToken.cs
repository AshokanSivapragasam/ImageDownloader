// -----------------------------------------------------------------------
// <copyright file="EmailInterchangeResponseToken.cs" company="Tcs">
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

    /// <summary>
    /// Class consists of Interchange send result
    /// </summary>
    public class EmailInterchangeResponseToken
    {
        /// <summary>
        /// Result for User Calling Enhance API methods
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