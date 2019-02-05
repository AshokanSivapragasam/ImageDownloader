using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// EmailData Class
    /// </summary>
    [DataContract]
    public class EmailData
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Email Subject
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Content ID
        /// </summary>
        [DataMember]
        public string ContentID { get; set; }

        /// <summary>
        /// Gets or sets the Created Date
        /// </summary>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Category ID
        /// </summary>
        [DataMember]
        public int CategoryID {get;set;}

        /// <summary>
        /// Gets or sets the Character Set
        /// </summary>
        [DataMember]
        public string CharacterSet {get;set;}

        /// <summary>
        /// Gets or sets the Cloned From ID
        /// </summary>
        [DataMember]
        public int ClonedFromID {get;set;}

        /// <summary>
        /// Gets or sets the Content Check Status
        /// </summary>
        [DataMember]
        public string ContentCheckStatus {get;set;}

        /// <summary>
        /// Gets or sets the Correlation ID
        /// </summary>
        [DataMember]
        public string CorrelationID {get;set;}
        
        /// <summary>
        /// Gets or sets the Customer Key
        /// </summary>
        [DataMember]
        public string CustomerKey {get;set;}

        /// <summary>
        /// Gets or sets the EmailType
        /// </summary>
        [DataMember]
        public string EmailType {get;set;}

        /// <summary>
        /// Gets or sets the Folder Name
        /// </summary>
        [DataMember]
        public string FolderName {get;set;}

        /// <summary>
        /// Gets or sets the HasDynamicSubjectLine
        /// </summary>
        [DataMember]
        public Boolean HasDynamicSubjectLine {get;set;}

        /// <summary>
        /// Gets or sets the HTMLBody
        /// </summary>
        [DataMember]
        public string HTMLBody {get;set;}

        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        [DataMember]
        public int ID {get;set;}

        /// <summary>
        /// Gets or sets the IsActive
        /// </summary>
        [DataMember]
        public Boolean IsActive {get;set;}

        /// <summary>
        /// Gets or sets the Is HTML Paste
        /// </summary>
        [DataMember]
        public Boolean IsHTMLPaste {get;set;}

        /// <summary>
        /// Gets or sets the Modified Date
        /// </summary>
        [DataMember]
        public DateTime? ModifiedDate {get;set;}
        
        /// <summary>
        /// Gets or sets the ObjectID
        /// </summary>
        [DataMember]
        public string ObjectID {get;set;}
        
        /// <summary>
        /// Gets or sets the PartnerKey
        /// </summary>
        [DataMember]
        public string PartnerKey {get;set;}
        
        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        [DataMember]
        public string Status {get;set;}

        /// <summary>
        /// Gets or sets the TextBody
        /// </summary>
        [DataMember]
        public string TextBody {get;set;}
    }

    /// <summary>
    /// EmailDataList Class
    /// </summary>
    [CollectionDataContract]
    public class EmailDataList : List<EmailData> { }

    /// <summary>
    /// RequestForReturnEmailList Class
    /// </summary>
    [DataContract]
    public class RequestForReturnEmailList
    {
        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        [DataMember]
        public DateTime Date  { get; set; }

        /// <summary>
        /// Gets or sets the TenantAccountID
        /// </summary>
        [DataMember]
        public int TenantAccountID { get; set; }

        /// <summary>
        /// Gets or sets the FolderId
        /// </summary>
        [DataMember]
        public int FolderId { get; set; }
    }

    /// <summary>
    /// RequestForRetrieveFolder Class
    /// </summary>
    [DataContract]
    public class RequestForRetrieveFolder
    {
        /// <summary>
        /// Gets or sets the ExactTargetAccountId
        /// </summary>
        [DataMember]
        public int ExactTargetAccountId { get; set; }
    }

    /// <summary>
    /// DataFolder Class
    /// </summary>
    [DataContract]
    public class DataFolder
    {
        /// <summary>
        /// Gets or sets the AllowChildren
        /// </summary>
        [DataMember]
        public bool AllowChildren{get;set;}

        /// <summary>
        /// Gets or sets the AllowChildrenSpecified
        /// </summary>
        [DataMember]
        public bool AllowChildrenSpecified { get; set; }

        /// <summary>
        /// Gets or sets the ContentType
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDateSpecified
        /// </summary>
        [DataMember]
        public bool CreatedDateSpecified { get; set; }

        /// <summary>
        /// Gets or sets the CustomerKey
        /// </summary>
        [DataMember]
        public string CustomerKey { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the CorrelationID
        /// </summary>
        [DataMember]
        public string CorrelationID { get; set; }

        /// <summary>
        /// Gets or sets the FolderID
        /// </summary>
        [DataMember]
        public int FolderID { get; set; }

        /// <summary>
        /// Gets or sets the IDSpecified
        /// </summary>
        [DataMember]
        public bool IDSpecified { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDate
        /// </summary>
        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDateSpecified
        /// </summary>
        [DataMember]
        public bool ModifiedDateSpecified { get; set; }

        /// <summary>
        /// Gets or sets the ObjectID
        /// </summary>
        [DataMember]
        public string ObjectID { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IsActive
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the IsActiveSpecified
        /// </summary>
        [DataMember]
        public bool IsActiveSpecified { get; set; }

        /// <summary>
        /// Gets or sets the IsEditable
        /// </summary>
        [DataMember]
        public bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets the IsEditableSpecified
        /// </summary>
        [DataMember]
        public bool IsEditableSpecified { get; set; }

        /// <summary>
        /// Gets or sets the FolderName
        /// </summary>
        [DataMember]
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the PartnerKey
        /// </summary>
        [DataMember]
        public string PartnerKey { get; set; }

        /// <summary>
        /// Gets or sets the ParentFolder
        /// </summary>
        [DataMember]
        public DataFolder ParentFolder { get; set; }
      }

    /// <summary>
    /// DataFolderList Class
    /// </summary>
    [CollectionDataContract]
    public class DataFolderList : List<DataFolder>
    { }     
}
