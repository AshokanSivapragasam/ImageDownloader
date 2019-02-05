using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core
{
    /// <summary>
    /// ETConnectionConfig Class
    /// </summary>
    public class ETConnectionConfig
    {   
        /// <summary>
        /// Gets or sets ExactTarget Service URL
        /// </summary>
        public string ExactTargetURL { get; set; }
        /// <summary>
        /// Gets or sets ExactTarget Service UserName
        /// </summary>
        public string ExactTargetUserName { get; set; }
        /// <summary>
        /// Gets or sets ExactTarget Service Password
        /// </summary>
        public string ExactTargetAccountPassword { get; set; }
        /// <summary>
        /// Gets or sets ExactTarget Service SFTP URL
        /// </summary>
        public string ExactTargetSFTPURL { get; set; }
        /// <summary>
        /// Gets or sets ExactTarget Service SFTP UserName 
        /// </summary>
        public string SFTPUserName { get; set; }
        /// <summary>
        /// Gets or sets ExactTarget Service SFTP Password
        /// </summary>
        public string SFTPPassword { get; set; }
        /// <summary>
        /// Gets or sets EnterpriseAccountID 
        /// </summary>
        public int EnterpriseAccountId { get; set; }      
        /// <summary>
        /// Gets or sets USe Certificate while interacting with ET
        /// </summary>
        public bool UseETCertificate { get; set; }
        /// <summary>
        /// Gets or sets ET Certificate Name
        /// </summary>
        public string ExactTargetCertificateName { get; set; }
        /// <summary>
        /// Gets or sets ET Certificate StoreName
        /// </summary>
        public string ExactTargetStoreName { get; set; }
        /// <summary>
        /// Gets or sets ET Certificate FindType
        /// </summary>
        public string ExactTargetX509FindType { get; set; }
        /// <summary>
        /// Gets or sets ET Certificate Store Location
        /// </summary>
        public string ExactTargetStoreLocation { get; set; }
        /// <summary>
        /// Gets or sets Client Certificate Name
        /// </summary>
        public string MSCertificateName { get; set; }
        /// <summary>
        /// Gets or sets Client Certificate StoreName
        /// </summary>
        public string MSStoreName { get; set; }
        /// <summary>
        /// Gets or sets Client Certificate  FindType
        /// </summary>
        public string Msx509FindType { get; set; }
        /// <summary>
        /// Gets or sets Client Certificate StoreLocation
        /// </summary>
        public string MSStoreLocation { get; set; }
        

    }
}
