using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AssetModeratorWebApi.Controllers
{
    public class AzureQueueMessageController : ApiController
    {
        [HttpPost]
        // POST: api/AzureQueueMessage
        public void Post([FromBody]string jsonContent)
        {
            #region AddMessageToQueue
            string queueConnectionString = "DefaultEndpointsProtocol=https;AccountName=cognitiveassetstorage;AccountKey=smfkI8QnP4xvFekC8fCmhjP2STYeEwzU+CoA9WSk3omIyfsy1SsSG1yyUt9S3VnTuBVWwL8uO1ZuWZPfyvMx6w==;EndpointSuffix=core.windows.net";
            string queueName = "mediacontentreceipts";
            CloudQueue cloudQueue = null;

            var isQueueConnected = AzureQueueHelper.CreateConnection(queueConnectionString, queueName, false, out cloudQueue);

            if (isQueueConnected)
                AzureQueueHelper.EnqueueMessage(cloudQueue, jsonContent);
            #endregion
        }
    }

    public class AzureQueueHelper
    {
        public static bool CreateConnection(string azureQueueConnectionString, string queueName, bool isCreateIfNotExists, out CloudQueue queue)
        {
            bool isConnected = false;
            CloudStorageAccount accountInfo = null;
            CloudQueueClient queueClient = null;
            queue = null;
            try
            {
                //Create service client for credentialed access to the Queue service.
                accountInfo = CloudStorageAccount.Parse(azureQueueConnectionString);

                //Create an instance to azure queues based on the account.
                queueClient = accountInfo.CreateCloudQueueClient();

                //Get a reference to a queue in this storage account.
                queue = queueClient.GetQueueReference(queueName);

                if (isCreateIfNotExists)
                {
                    //Create the queue if it does not already exist.
                    queue.CreateIfNotExists();
                }

                isConnected = true;
            }
            catch (Exception ex)
            {
                isConnected = false;
                Console.WriteLine("Error-CreateConnection(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
            }

            return isConnected;
        }

        /// <summary>
        /// Gets the total number of messages in queue in configured intervals
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="jsonContent"></param>
        public static void EnqueueMessage(CloudQueue queue, string jsonContent)
        {
            Console.WriteLine("Monitoring the queue, '" + queue.Name + "'. It will add #msg to queue");
            try
            {
                queue.AddMessage(new CloudQueueMessage(jsonContent));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error-Enqueue(): " + ex.Message.Replace("\r", " ").Replace("\n", " "));
            }
        }
    }
}
