using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AssetModeratorWebApi.Controllers
{
    public class AzureDocumentDbItemController : ApiController
    {
        private static readonly string endpointUrl = "https://cognitiveassetscosmosdb.documents.azure.com:443";
        private static readonly string authorizationKey = "Txxlu9mwdokCu3lmAwi3onYWpmVpB32i5D3VR25faEmvoBrWBPr8lXhkVnfZdOxeIyquDUSLCdevphDnLCtMGA==";
        private static readonly string databaseId = "MediaContentAnalysis";
        private static readonly string collectionId = "MediaProperties";

        private static DocumentClient client;

        [HttpGet]
        // POST: api/AzureBlobItem
        public async Task<HttpResponseMessage> Get()
        {
            if (client == null)
                client = new DocumentClient(new Uri(endpointUrl), authorizationKey);

            AzureDocumentDbCrudHelper.Init(client, databaseId, collectionId);

            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
            
            var _documents_ = await client.ReadDocumentFeedAsync(collectionLink);
            var _noOfDocuments_ = _documents_.Count();

            //AzureDocumentDbCrudHelper
            return Request.CreateResponse(HttpStatusCode.OK, new { noOfDocuments = _noOfDocuments_, documents = _documents_ });
        }

        [HttpPost]
        // POST: api/AzureBlobItem
        public async Task<HttpResponseMessage> Post([FromBody]string jsonContent)
        {
            if (client == null)
                client = new DocumentClient(new Uri(endpointUrl), authorizationKey);

            AzureDocumentDbCrudHelper.Init(client, databaseId, collectionId);

            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

            //Create a dynamic object,
            //Notice the case here. The properties will be created in the database
            //with whatever case you give the property
            //If you read this back in to a Document object id will get mapped to Id
            dynamic dynamicJObject = JObject.Parse(jsonContent);
            
            ResourceResponse<Document> response = await client.CreateDocumentAsync(collectionLink, dynamicJObject);
            var createdDocument = response.Resource;
            
            //AzureDocumentDbCrudHelper
            return Request.CreateResponse(HttpStatusCode.OK, new { createdDocumentId = createdDocument.Id, responseRequestCharge = response.RequestCharge });
        }
    }
}
