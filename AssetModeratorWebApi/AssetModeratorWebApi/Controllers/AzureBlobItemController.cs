using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AssetModeratorWebApi.Controllers
{
    public class AzureBlobItemController : ApiController
    {
        [HttpPost]
        // POST: api/AzureBlobItem
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

            var _fileUploaded_ = new List<string>();
            var blobContainerConnectionString = "DefaultEndpointsProtocol=https;AccountName=cognitiveassetstorage;AccountKey=smfkI8QnP4xvFekC8fCmhjP2STYeEwzU+CoA9WSk3omIyfsy1SsSG1yyUt9S3VnTuBVWwL8uO1ZuWZPfyvMx6w==;EndpointSuffix=core.windows.net";
            var blobContainerName = "mediacontentfiles";
            var blobImageUrl = "https://cognitiveassetstorage.blob.core.windows.net/mediacontentfiles/";
            var path = System.Web.Hosting.HostingEnvironment.MapPath("~");
            var provider = AzureBlobHelper.GetMultipartProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.FileData)
            {
                _fileUploaded_.Add(file.Headers.ContentDisposition.FileName);
                var localFilePath = new FileInfo(file.LocalFileName);
                var targetBlobName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                blobImageUrl += targetBlobName;
                AzureBlobHelper.UploadFileToBlob(blobContainerConnectionString, blobContainerName, localFilePath.FullName, targetBlobName);
                File.Delete(file.LocalFileName);
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { blobImageUrl = blobImageUrl , filesUploaded = _fileUploaded_ });
        }
    }

    public class AzureBlobHelper
    {
        public static MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudblobcontainer"></param>
        /// <param name="localFilePath"></param>
        public static void UploadFileToBlob(string blobContainerConnectionString, string blobContainerName, string localFilePath, string targetBlobName)
        {
            var cloudblobcontainer = CloudStorageAccount.Parse(blobContainerConnectionString)
                             .CreateCloudBlobClient()
                             .GetContainerReference(blobContainerName);

            CloudBlockBlob cloudblockblob = cloudblobcontainer.GetBlockBlobReference(targetBlobName);
            using (FileStream fs = new FileStream(localFilePath, FileMode.Open))
            {
                cloudblockblob.UploadFromStream(fs);
            }
        }
    }
}
