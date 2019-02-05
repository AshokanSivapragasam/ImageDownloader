using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// GenericSubscriber Class
    /// </summary>
    [DataContract]
    public class GenericSubscriber : SubscriberBase
    {
        /// <summary>
        /// Constructor for GenericSubscriber Class
        /// </summary>
        public GenericSubscriber()
            : base()
        {

        }
    }
}
