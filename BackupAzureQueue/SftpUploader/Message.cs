using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SftpUploader
{
    /*public class Message : TableServiceEntity
    {
        public string LogMessage { get; set; }

        public Message()
        {
        }

        public Message(int MessagePartionKey, string logMessage)
        {
            PartitionKey = MessagePartionKey.ToString();
            Timestamp = DateTime.Now;
            RowKey = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + " | " + Guid.NewGuid().ToString();
            LogMessage = logMessage;
        }
    }

    public class Logger
    {
        static CloudStorageAccount account;
        static TableServiceContext tableContext;
        static string loggerTableName;

        static Logger()
        {
            string storageAcConnectionString = CloudConfigurationManager.GetSetting("storageAcConnectionString");
            string storageTableName = CloudConfigurationManager.GetSetting("storageTableName");
            if (CloudStorageAccount.TryParse(storageAcConnectionString, out account))
            {
                CloudTableClient tableClient = account.CreateCloudTableClient();
                tableClient.CreateTableIfNotExist(storageTableName);
                tableContext = tableClient.GetDataServiceContext();
                loggerTableName = storageTableName;
            }
            else
                Logger.AddMessage(new Message(0, "StorageAcConnectionString is not parsable, '" + storageAcConnectionString + "'"));
        }

        public static void AddMessage(Message logMessage)
        {
            tableContext.AddObject(loggerTableName, logMessage);
            tableContext.SaveChangesWithRetries();
        }
    }*/
}
