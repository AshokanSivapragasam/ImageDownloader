using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// LimitedProgramSubscriber Class
    /// </summary>
    [DataContract]
    public class LimitedProgramSubscriber : SubscriberBase
    {
        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        [DataMember]
        public String FirstName { get; set; }

        /// <summary>
        /// Gets or sets the MiddleName
        /// </summary>
        [DataMember]
        public String MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the LastName1
        /// </summary>
        [DataMember]
        public String LastName1 { get; set; }

        /// <summary>
        /// Gets or sets the LastName2
        /// </summary>
        [DataMember]
        public String LastName2 { get; set; }

        /// <summary>
        /// Gets or sets the NamePrefix
        /// </summary>
        [DataMember]
        public String NamePrefix { get; set; }

        /// <summary>
        /// Gets or sets the NameSuffix
        /// </summary>
        [DataMember]
        public String NameSuffix { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted
        /// </summary>
        [DataMember]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the RegistrationDate
        /// </summary>
        [DataMember]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the WizardData
        /// </summary>
        [DataMember]
        public string WizardData { get; set; }

        /// <summary>
        /// Gets or sets the QuestionIds
        /// </summary>
        [DataMember]
        public Collection<Guid> QuestionIds { get; private set; }

        /// <summary>
        /// Gets or sets the WizardIds
        /// </summary>
        [DataMember]
        public Collection<Guid> WizardIds { get; private set; }

        /// <summary>
        /// Constructor for LimitedProgramSubscriber
        /// </summary>
        public LimitedProgramSubscriber()
            : base()
        {

        }

        /// <summary>
        /// AddQuestionId Method is being used for AddQuestionId
        /// </summary>
        /// <param name="questionId"></param>
        public void AddQuestionId(Guid questionId)
        {
            if (this.QuestionIds == null) this.QuestionIds = new Collection<Guid>();
            this.QuestionIds.Add(questionId);
        }

        /// <summary>
        /// AddWizardId Method is being used for AddWizardId
        /// </summary>
        /// <param name="wizardId"></param>
        public void AddWizardId(Guid wizardId)
        {
            if (this.WizardIds == null) this.WizardIds = new Collection<Guid>();
            this.WizardIds.Add(wizardId);
        }
    }
}
