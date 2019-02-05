using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core.Enumerators
{
    /// <summary>
    /// Enum RequestType
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// CommunicationSetupData
        /// </summary>
        CommunicationSetupData = 1,

        /// <summary>
        /// CampaignMetaData
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MetaData")]
        CampaignMetaData = 2,

        /// <summary>
        /// WizardData 
        /// </summary>
        WizardData = 3,

        /// <summary>
        /// SubscriptionData 
        /// </summary>
        SubscriptionData = 4,

        /// <summary>
        /// CustomerRefreshData 
        /// </summary>
        CustomerRefreshData = 5,

        /// <summary>
        /// EmailResponseData 
        /// </summary>
        EmailResponseData = 6,

        /// <summary>
        /// SuppressionPromotionalData 
        /// </summary>
        SuppressionPromotionalData = 7,

        /// <summary>
        /// PromotionalListData 
        /// </summary>
        PromotionalListData = 11,

        /// <summary>
        /// TBNData
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TBN")]
        TBNData = 12,

        /// <summary>
        /// LimitedProgramData 
        /// </summary>
        LimitedProgramData = 13,

        /// <summary>
        /// SuppressionTransactionalData 
        /// </summary>
        SuppressionTransactionalData = 14,

        /// <summary>
        /// ReportExtractData 
        /// </summary>
        ReportExtractData = 15,

        /// <summary>
        /// DeleteSubscription
        /// </summary>
        DeleteSubscription = 16,

        /// <summary>
        /// BulkSendData
        /// </summary>
        BulkSendData = 17,

        /// <summary>
        /// Following two MSI request types added by "v-rachen", which will be used for MSI Service calls
        /// </summary>
        MSISubscribe = 18,

        /// <summary>
        /// Following two MSI request types added by "v-rachen", which will be used for MSI Service calls
        /// </summary>
        MSIUnsubscribe = 19,
                    
        /// <summary>
        /// FileTrigger request type added by "v-apola" 
        /// </summary>
        FileTriggerData = 20
    }
    
    /// <summary>
    /// Enum BulkSendEmailType
    /// </summary>
    public enum BulkSendEmailType
    {
        /// <summary>
        /// Promotional 
        /// </summary>
        Promotional = 0,

        /// <summary>
        /// Transactional 
        /// </summary>
        Transactional = 1
    }

    /// <summary>
    /// Enum DataImportType
    /// </summary>
    public enum DataImportType
    {
      /// <summary>
        /// Overwrite 
      /// </summary>
       Overwrite = 0,

        /// <summary>
       /// AddAndUpdate 
        /// </summary>
       AddAndUpdate = 1
    }
}
