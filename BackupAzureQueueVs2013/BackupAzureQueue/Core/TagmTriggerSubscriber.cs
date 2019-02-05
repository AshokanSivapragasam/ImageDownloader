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
    /// TagmTriggerSubscriber Class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tagm")]
    [DataContract]
    [Serializable]
    public class TagmTriggerSubscriber : SubscriberBase
    {
        /// <summary>
        /// Get or Sets the FirstName
        /// </summary>
        [DataMember(IsRequired = false)]
        public String FirstName { get; set; }

        /// <summary>
        /// Get or Sets the MiddleName
        /// </summary>
        [DataMember(IsRequired = false)]
        public String MiddleName { get; set; }

        /// <summary>
        /// Get or Sets the LastName1
        /// </summary>
        [DataMember(IsRequired = false)]
        public String LastName1 { get; set; }

        /// <summary>
        /// Get or Sets the LastName2
        /// </summary>
        [DataMember(IsRequired = false)]
        public String LastName2 { get; set; }

        /// <summary>
        /// Get or Sets the NamePrefix
        /// </summary>
        [DataMember(IsRequired = false)]
        public String NamePrefix { get; set; }

        /// <summary>
        /// Get or Sets the NameSuffix
        /// </summary>
        [DataMember(IsRequired = false)]
        public String NameSuffix { get; set; }

        /// <summary>
        /// Get or Sets the RegistrationDate
        /// </summary>
        [DataMember(IsRequired = false)]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Get or Sets the WizardData
        /// </summary>
        [DataMember(IsRequired = false)]
        public string WizardData { get; set; }

        /// <summary>
        /// Get or Sets the QuestionIds
        /// </summary>
        [DataMember(IsRequired = false)]
        public Collection<Guid> QuestionIds { get; private set; }

        /// <summary>
        /// Get or Sets the WizardIds
        /// </summary>
        [DataMember(IsRequired = false)]
        public Collection<Guid> WizardIds { get; private set; }

        /// <summary>
        /// Constructor for TagmTriggerSubscriber Class
        /// </summary>
        public TagmTriggerSubscriber()
            : base()
        {

        }

        /// <summary>
        /// AddQuestionId Method
        /// </summary>
        /// <param name="questionId"></param>
        public void AddQuestionId(Guid questionId)
        {
            if (this.QuestionIds == null) this.QuestionIds = new Collection<Guid>();
            this.QuestionIds.Add(questionId);
        }

        /// <summary>
        /// AddWizardId Method
        /// </summary>
        public void AddWizardId(Guid wizardId)
        {
            if (this.WizardIds == null) this.WizardIds = new Collection<Guid>();
            this.WizardIds.Add(wizardId);
        }
    }
}