using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AzureTBNClientSDK = Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.AzureServiceReference;
using System.IO;

namespace WcfSvcApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EmailInterchangeApi : IEmailInterchangeApi
    {
        public bool Ping()
        {
            return true;
        }

        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public bool RaiseBulkSendRequest()
        {
            var srcFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile.tsv";
            var destFileName = @"D:\Usr\Ajit\IamRaveBulkSendDataFile_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".tsv";
            File.Copy(srcFileName, destFileName, true);
            try
            {
                Console.WriteLine(" Counter -- '' BulkSend() -- " + BulkSend(
                    batchId: "0",
                    bulkSendId: "1",
                    contentId: 350882, //351062(10900830),//41411(39327), //350882, //Dynamic email content 350762, //Static Email Content - 350207,
                    filePath: destFileName,
                    tenantAccountId: "10460681",
                    bulkSendEmailType: BulkSendEmailType.Transactional,
                    isSendInvoke: true,
                    isDynamicDataExtension: true,
                    isOverrideConfiguration: true,
                    dataImportType: DataImportType.Overwrite,
                    dynamicDataExtensionTemplateName: "TriggeredSendDataExtension"));
            }
            catch (Exception ex) { Console.WriteLine(" BulkSend() - " + ex.Message.Replace("\r", " ").Replace("\n", " ")); }

            return true;
        }

        #region BULKSEND_METHODS
        /// <summary>
        /// Test Method for BulkSend
        /// </summary>
        /// <returns></returns>
        private static string BulkSend(string batchId, string bulkSendId, int contentId, string filePath, string tenantAccountId, BulkSendEmailType bulkSendEmailType, bool isSendInvoke, bool isDynamicDataExtension, bool isOverrideConfiguration, DataImportType dataImportType, string dynamicDataExtensionTemplateName)
        {
            //"b480ee7d-2444-4f37-ba6d-db279b87b60a","b480ee7d-2444-4f37-ba6d-db279b87b60a",@"D:\Usr\ShankarBaradwaj\MslMailerBulkSendList-20150313-255746814.tsv","10290011",BulkSendEmailType.Transactional,true, true,true,DataImportType.AddAndUpdate,"MSL Email Campaigns DE"
            var fileRequest = new FileRequest()
            {
                BatchId = batchId,
                BulkSendId = bulkSendId,
                ContentId = contentId,
                FilePath = filePath,
                TenantAccountId = tenantAccountId,
                BulkSendEmailType = bulkSendEmailType,
                IsSendInvoke = isSendInvoke,
                IsDynamicDataExtension = isDynamicDataExtension,
                IsOverrideConfiguration = isOverrideConfiguration,
                DataImportType = dataImportType,
                DynamicDataExtensionTemplateName = dynamicDataExtensionTemplateName
            };

            var azureTBNClient = new AzureTBNClientSDK.InterchangeConnect();
            var filesendResult = azureTBNClient.SendFileRequest(fileRequest);
            return (filesendResult.Result + "; EI ID: '" + filesendResult.EmailInterchangeId + "'");
        }
        #endregion
    }
}
