using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// InterchangeFileRequest Class
    /// </summary>
    [DataContract]
    [Serializable]
    public class InterchangeFileRequest : Request, IRequest
    {
        /// <summary>
        /// Constructor of InterchangeFileRequest Class
        /// </summary>
        /// <param name="fileRequest"></param>
        public InterchangeFileRequest(FileRequest fileRequest)
            : base()
        {
            this.EmailInterchangeRequestId = Guid.NewGuid();
            this.StartDateTime = DateTime.UtcNow;
            this.EndDateTime = DateTime.UtcNow;

            this.CurrentRetryAttempt = 0;
            this.MaxRetryAttempts = 6;

            if (fileRequest != null)
            {
                ContentId = fileRequest.ContentId;
                BulkSendId = fileRequest.BulkSendId;
                TenantAccountId = fileRequest.TenantAccountId;
                BatchId = fileRequest.BatchId;
                FilePath = fileRequest.FilePath;
                Type = fileRequest.Type;
                ScheduledDateTime = fileRequest.ScheduledDateTime;
                FriendlyFromName = fileRequest.FriendlyFromName;
                BulkSendEmailType = fileRequest.BulkSendEmailType;
                if (!fileRequest.IsSendInvoke.HasValue)
                {
                    IsSendInvoke = true;
                }
                else 
                {
                    IsSendInvoke =(bool)fileRequest.IsSendInvoke;
                }
                IsDynamicDataExtension = fileRequest.IsDynamicDataExtension;
                DynamicDataExtensionTemplateName = fileRequest.DynamicDataExtensionTemplateName;
                IsOverrideConfiguration = fileRequest.IsOverrideConfiguration;
                DataImportType = fileRequest.DataImportType;
            }
        }

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
        public bool IsSendInvoke { get; set; }

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
