using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TamilYogiWebApi.Models;
using AzureTBNClientSDK = Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Clients.Sdk.AzureServiceReference;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Headers;
using System.IO;

namespace TamilYogiWebApi.Controllers
{
    public class EiRequestsController : ApiController
    {
        // OPTIONS
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        // GET api/<controller>
        [HttpPost]
        [ResponseType(typeof(FileRequestSendResult))]
        public IHttpActionResult AddBulksendRequest(BulksendRequestViewModel bulksendRequestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bulksendRequestViewModel.BulksendRequestId = 1;
            bulksendRequestViewModel.EmailSendScheduleDatetime = DateTime.Now;

            var fileRequest = new FileRequest()
            {
                BatchId = bulksendRequestViewModel.BatchId,
                BulkSendId = bulksendRequestViewModel.BulksendId,
                ContentId = bulksendRequestViewModel.EmailContentId,
                FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/datafiles/") + bulksendRequestViewModel.BulksendInputDataFile,
                TenantAccountId = bulksendRequestViewModel.TenantAccountId,
                BulkSendEmailType = bulksendRequestViewModel.EmailClassification ? BulkSendEmailType.Promotional : BulkSendEmailType.Transactional,
                IsSendInvoke = bulksendRequestViewModel.IsEmailSendInvoke,
                IsDynamicDataExtension = bulksendRequestViewModel.BulksendApproach,
                IsOverrideConfiguration = true,
                DataImportType = bulksendRequestViewModel.DataImportType ? DataImportType.Overwrite : DataImportType.AddAndUpdate,
                DynamicDataExtensionTemplateName = bulksendRequestViewModel.DataExtensionTemplateName
            };

            var azureTBNClient = new AzureTBNClientSDK.InterchangeConnect();
            var filesendResult = azureTBNClient.SendFileRequest(fileRequest);

            return CreatedAtRoute("EiRequestsApi", null, filesendResult);
        }
    }
}