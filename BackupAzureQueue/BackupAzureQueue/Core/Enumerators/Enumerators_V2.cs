using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators
{
    /// <summary>
    /// Enum EmailType
    /// </summary>
    public enum EmailType
    {
        /// <summary>
        /// Text
        /// </summary>
        Text,

        /// <summary>
        /// Html
        /// </summary>
        Html
    }

    /// <summary>
    /// Enum TriggeredSendType
    /// </summary>
    public enum TriggeredSendType
    {
        /// <summary>
        /// Delay
        /// </summary>
        Delay,

        /// <summary>
        /// NoDelay
        /// </summary>
        NoDelay,

        /// <summary>
        /// Batch
        /// </summary>
        Batch
    }

    /// <summary>
    /// Enum TriggerDataSource
    /// </summary>
    public enum TriggerDataSource
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// MSIndividual
        /// </summary>
        MSIndividual,

        /// <summary>
        /// File
        /// </summary>
        File,

        /// <summary>
        /// List
        /// </summary>
        List
    }

    /// <summary>
    /// Enum EmailInterchangeResult
    /// </summary>
    public enum EmailInterchangeResult
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// Success
        /// </summary>
        Success,

        /// <summary>
        /// UnknownFailure
        /// </summary>
        UnknownFailure,

        /// <summary>
        /// InvalidRequest
        /// </summary>
        InvalidRequest,

        /// <summary>
        /// InvalidEmailAddress
        /// </summary>
        InvalidEmailAddress,

        /// <summary>
        /// InvalidSubscriberKey
        /// </summary>
        InvalidSubscriberKey,

        /// <summary>
        /// InvalidCommunicationId
        /// </summary>
        InvalidCommunicationId,

        /// <summary>
        /// Unauthorized
        /// </summary>
        Unauthorized,

        /// <summary>
        /// NoImplementation
        /// </summary>
        NoImplementation,

        /// <summary>
        /// InvalidContentId
        /// </summary>
        InvalidContentId,

        /// <summary>
        /// InvalidBulkSendId
        /// </summary>
        InvalidBulkSendId,

        /// <summary>
        /// InvalidTenantAccountId
        /// </summary>
        InvalidTenantAccountId,

        /// <summary>
        /// InvalidBatchId
        /// </summary>
        InvalidBatchId,

        /// <summary>
        /// InvalidFileType
        /// </summary>
        InvalidFileType,

        /// <summary>
        /// FilePath
        /// </summary>
        FilePath,

        /// <summary>
        /// MissingConfiguration
        /// </summary>
        MissingConfiguration,

        /// <summary>
        /// InvalidAccountId
        /// </summary>
        InvalidAccountId,

        /// <summary>
        /// InvalidCommIdOrCustomerKey
        /// </summary>
        InvalidCommIdOrCustomerKey,

        /// <summary>
        /// InvalidTemplateIdOrCustomerKey
        /// </summary>
        InvalidTemplateIdOrCustomerKey
    }

    /// <summary>
    /// Enum QueueDomain
    /// </summary>
    public enum QueueDomain
    {
        /// <summary>
        /// Default
        /// </summary>
        Default,

        /// <summary>
        /// Corpnet
        /// </summary>
        Corpnet,

        /// <summary>
        /// Phoenix
        /// </summary>
        Phoenix
    }
}
