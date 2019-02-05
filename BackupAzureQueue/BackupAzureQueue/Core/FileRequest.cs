using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// FileRequest Class
    /// </summary>
    [DataContract]
    [Serializable]
    public class FileRequest : RequestBase
    {
        /// <summary>
        /// Gets or sets the ContentId
        /// </summary>
        [DataMember]
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets the BulkSendId
        /// </summary>
        [DataMember]
        public string BulkSendId { get; set; }

        /// <summary>
        /// Gets or sets the TenantAccountId
        /// </summary>
        [DataMember]
        public string TenantAccountId { get; set; }

        /// <summary>
        /// Gets or sets the BatchId
        /// </summary>
        [DataMember]
        public string BatchId { get; set; }

        /// <summary>
        /// Gets or sets the FilePath
        /// </summary>
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        [DataMember]
        public RequestType Type { get; set; }

        /// <summary>
        /// Gets or sets the ScheduledDateTime
        /// </summary>
        [DataMember]
        public DateTime ScheduledDateTime { get; set; }

        /// <summary>
        /// Gets or sets the FriendlyFromName
        /// </summary>
        [DataMember]
        public string FriendlyFromName { get; set; }

        /// <summary>
        /// Gets or sets the BulkSendEmailType
        /// </summary>
        [DataMember]
        public BulkSendEmailType BulkSendEmailType { get; set; }

        /// <summary>
        /// Gets or sets the IsSendInvoke
        /// </summary>
        [DataMember]
        public bool? IsSendInvoke { get; set; }

        /// <summary>
        /// Gets or sets the IsDynamicDataExtension
        /// </summary>
        [DataMember]
        public bool IsDynamicDataExtension { get; set; }

        /// <summary>
        /// Gets or sets the IsOverrideConfiguration
        /// </summary>
        [DataMember]
        public bool IsOverrideConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the DynamicDataExtensionTemplateName
        /// </summary>
        [DataMember]
        public string DynamicDataExtensionTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the DataImportType
        /// </summary>
        [DataMember]
        public DataImportType DataImportType { get; set; }      

    }
}
