using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// Enum RequestExecutionPriorityLevels
    /// </summary>
    public enum RequestExecutionPriorityLevels
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// High
        /// </summary>
        High = 2,          //SLA = 2 Minutes
        /// <summary>
        /// Medium
        /// </summary>
        Medium = 3,        //SLA = 3 Minutes
        /// <summary>
        /// Low
        /// </summary>
        Low = 4           //SLA = 4 Hours
    }

    /// <summary>
    /// RequestBase Class
    /// </summary>
    [DataContract]
    [KnownType(typeof(Request))]
    [KnownType(typeof(TriggeredRequestBase))]
    [KnownType(typeof(EventRequestData))]
    [KnownType(typeof(TriggeredRequest))]
    [KnownType(typeof(PartnerTriggerSubscriber))]
    [KnownType(typeof(LimitedProgramSubscriber))]
    [KnownType(typeof(TagmTriggerSubscriber))]
    [KnownType(typeof(GenericSubscriber))]
    public abstract class RequestBase
    {
    }
}
