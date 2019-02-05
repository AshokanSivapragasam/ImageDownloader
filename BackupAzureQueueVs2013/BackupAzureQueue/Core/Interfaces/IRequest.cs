using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Interfaces
{
    /// <summary>
    /// Defines the attributes required for processing any request by Email Interchange
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Gets the EI Request ID
        /// </summary>
        [DataMember(IsRequired = false)]
        Guid EmailInterchangeRequestId { get; }

        /// <summary>
        /// Gets or sets the ET Request Id
        /// </summary>
        [DataMember(IsRequired = false)]
        Guid ExactTargetRequestId { get; set; }

        /// <summary>
        /// Gets the ComponentStates
        /// </summary>
        [DataMember(IsRequired = false)]
        Dictionary<string, object> ComponentStates { get; }

        /// <summary>
        /// Gets or sets the RequestType
        /// </summary>
        [DataMember(IsRequired = false)]
        RequestType RequestType { get; set; }

        /// <summary>
        /// Gets or sets the TransactionType
        /// </summary>
        [DataMember(IsRequired = false)]
        TransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the RequestError
        /// </summary>
        [DataMember(IsRequired = false)]
        ErrorSource RequestError { get; set; }

        /// <summary>
        /// Gets or sets the OperationStatus
        /// </summary>
        [DataMember(IsRequired = false)]
        RequestStatus OperationStatus { get; set; }
        
        /// <summary>
        /// Gets or sets the Process Status (Ex: EIReceiveSuccess)
        /// </summary>        
        [DataMember(IsRequired = false)]
        ProcessStatus ProcessStatus { get; set; }

        /// <summary>
        /// Gets or Sets the DateTime stamp when the request execution was started
        /// </summary>
        [DataMember(IsRequired = false)]
        DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or Sets the DateTime stamp when the request execution was finished
        /// </summary>
        [DataMember(IsRequired = false)]
        DateTime EndDateTime { get; set; }

        /// <summary>
        /// Gets or Sets the Maximum retry attempts of particular request type
        /// </summary>
        [DataMember(IsRequired = false)]
        Int32 MaxRetryAttempts { get; set; }

        /// <summary>
        /// Gets or Sets the Current retry attempt of particular request
        /// </summary>
        [DataMember(IsRequired = false)]
        Int32 CurrentRetryAttempt { get; set; }

        /// <summary>
        /// Gets or Sets delay interval of particular request
        /// </summary>
        [DataMember(IsRequired = false)]
        Int32 DelayBeforeRetry { get; set; }

        /// <summary>
        /// Gets or Sets Account Id of particular request
        /// </summary>
        [DataMember(IsRequired = false)]
        string AccountId { get; set; }

        /// <summary>
        /// Get or Sets EnterpriseAccountId associated with the request
        /// </summary>
        [DataMember(IsRequired = false)]
        int EnterpriseAccountId { get; set; }

        /// <summary>
        /// Date when this request would be considered for execution, if it is lesser than current date, request would be picked up immediately
        /// </summary>
        [DataMember(IsRequired = false)]
        DateTime ScheduledDate { get; set; }

        /// <summary>
        /// AddOrUpdateComponentState Method
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddOrUpdateComponentState(string key, object value);
    }
}
