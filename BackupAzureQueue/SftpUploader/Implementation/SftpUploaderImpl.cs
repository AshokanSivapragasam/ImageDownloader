using Jscape.Sftp;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SftpUploader.Implementation
{
    /*public class SftpUploaderImpl
    {
        public static int noOfRuns;

        /// <summary>
        /// Get <c>CloudBlobClient</c> object after configuring <c>CloudStorageAccount</c>
        /// <param name="buffersize">Set the buffer size (Size of chunck sent per call</param>
        /// </summary>
        /// <returns>Returns the <c>CloudBlobContainer</c></returns>
        public Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer GetCloudBlobContainer(string storageAcConnectionString, string blobContainerName, double expBackoffTime, int maxRetryAttempts, int maxExecutionTime, int parallelOperationThreadCount, int buffersize = 1024 * 1024)
        {
            Microsoft.WindowsAzure.Storage.CloudStorageAccount cloudStorageAccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(storageAcConnectionString);
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            ExponentialRetry retrypolicy = new ExponentialRetry(TimeSpan.FromSeconds(expBackoffTime), maxRetryAttempts);

            // Set the buffer size (Size of chuncks sent per call)
            cloudBlobClient.SingleBlobUploadThresholdInBytes = buffersize;
            cloudBlobClient.RetryPolicy = retrypolicy;
            cloudBlobClient.MaximumExecutionTime = TimeSpan.FromMinutes(maxExecutionTime);
            cloudBlobClient.ParallelOperationThreadCount = parallelOperationThreadCount;

            // Get a reference to a container, which may or may not exist.
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainerName);

            // Create a new container, if it does not exist
            cloudBlobContainer.CreateIfNotExists();

            return cloudBlobContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageAcConnectionString"></param>
        /// <param name="blobContainerName"></param>
        /// <returns></returns>
        public CloudBlobContainer GetCloudBlobContainer(string storageAcConnectionString, string blobContainerName)
        {
            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Getting blob container instance.."));

            var storageAccount = CloudStorageAccount.Parse(storageAcConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(blobContainerName);

            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Got a blob container instance.."));

            return blobContainer;
        }

        /// <summary>
        /// Transfer content from blob to SFTP server
        /// </summary>
        /// <param name="blobContainer"></param>
        /// <param name="blobName"></param>
        /// <param name="sftpServer"></param>
        /// <param name="sftpUser"></param>
        /// <param name="sftpPassword"></param>
        /// <param name="sftpTimeout"></param>
        /// <param name="licenseKey"></param>
        /// <param name="ftpDirectory"></param>
        /// <returns></returns>
        public bool TransferBlobToSFTP(CloudBlobContainer blobContainer, string blobName, string sftpServer, string sftpUser, string sftpPassword, int sftpTimeout, string licenseKey, string ftpDirectory)
        {
            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Transfer a blob to sftp started.."));

            var blobToUpload = blobContainer.GetBlockBlobReference(blobName);

            #region DEPRECATED*/
            /*byte[] mfileBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                blobToUpload.DownloadToStream(ms);
                mfileBytes = ms.ToArray();
            }*/
            /*#endregion

            TimeSpan ts = new TimeSpan(10, 30, 0);
            blobToUpload.ServiceClient.Timeout = ts;
            byte[] fileBytes = blobToUpload.DownloadByteArray();
            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Downloaded blob as data bytes. Length - " + fileBytes.Length + " byte(s)"));

            UploadBytesToSftp(sftpServer, sftpUser, sftpPassword, sftpTimeout, licenseKey, ftpDirectory, fileBytes, "EiDevTeam_File_" + blobName + "_" + SftpUploaderImpl.noOfRuns.ToString("0000") + Path.GetExtension(blobName));

            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Transfer a blob to sftp successful.."));

            return true;
        }

        /// <summary>
        /// Uploads bytes to SFTP server
        /// </summary>
        /// <param name="sftpServer"></param>
        /// <param name="sftpUser"></param>
        /// <param name="sftpPassword"></param>
        /// <param name="sftpTimeout"></param>
        /// <param name="licenseKey"></param>
        /// <param name="ftpDirectory"></param>
        /// <param name="blobContent"></param>
        /// <param name="blobName"></param>
        public void UploadBytesToSftp(string sftpServer, string sftpUser, string sftpPassword, int sftpTimeout, string licenseKey, string ftpDirectory, byte[] blobContent, string blobName)
        {
            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Connecting to sftp."));
            Jscape.Ssh.SshParameters connectionParameters = new Jscape.Ssh.SshParameters(sftpServer, sftpUser, sftpPassword);
            Sftp fileTransfer = new Sftp(connectionParameters);
            fileTransfer.LicenseKey = licenseKey;

            fileTransfer.Connect();
            fileTransfer.SetBinaryMode();
            fileTransfer.RemoteDir = ftpDirectory;
            fileTransfer.Timeout = sftpTimeout;
            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Connected to sftp. server: '" + sftpServer + "' user: '" + sftpUser + "' dir: '" + ftpDirectory + "' filename: '" + blobName + "' ftptimeout: '" + fileTransfer.Timeout + "'"));
            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Upload started."));
            fileTransfer.Upload(blobContent, blobName);
            
            Logger.AddMessage(new Message(SftpUploaderImpl.noOfRuns, "Upload completed."));
        }
    }*/
}
