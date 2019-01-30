using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetModeratorWebApi.Controllers
{
    // ----------------------------------------------------------------------------------------------------------
    // Prerequistes - 
    // 
    // 1. An Azure DocumentDB account - 
    //    https://azure.microsoft.com/en-us/documentation/articles/documentdb-create-account/
    //
    // 2. Microsoft.Azure.DocumentDB NuGet package - 
    //    http://www.nuget.org/packages/Microsoft.Azure.DocumentDB/ 
    // ----------------------------------------------------------------------------------------------------------
    // Sample - demonstrates the basicCRUD operations on a Document resource for Azure DocumentDB
    //
    // 1. Basic CRUD operations on a document using regular POCOs
    // 1.1 - Create a document
    // 1.2 - Read a document by its Id
    // 1.3 - Read all documents in a Collection
    // 1.4 - Query for documents by a property other than Id
    // 1.5 - Replace a document
    // 1.6 - Upsert a document
    // 1.7 - Delete a document
    //
    // 2. Work with dynamic objects
    //-----------------------------------------------------------------------------------------------------------
    // See Also - 
    //
    // DocumentDB.Samples.QueryingDocuments - We only included a VERY basic query here for completeness,
    //                                        For a detailed exploration of how to query for Documents, 
    //                                        inlcuding how to paginate results of queries.
    //
    // DocumentDB.Samples.ServerSideScripts - In these examples we do simple loops to create small numbers
    //                                        of documents. For insert operations where you are creating many
    //                                        documents we recommend using a Stored Procedure and pass batches
    //                                        of new documents to this sproc. Consult this sample for an example
    //                                        of a BulkInsert stored procedure. 
    // ----------------------------------------------------------------------------------------------------------

    public class AzureDocumentDbCrudHelper
    {
#if do_not_use
        //Read config
        private static readonly string endpointUrl = ConfigurationManager.AppSettings["EndPointUrl"];
        private static readonly string authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];
        private static readonly string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private static readonly string collectionId = ConfigurationManager.AppSettings["CollectionId"];
        private static readonly ConnectionPolicy connectionPolicy = new ConnectionPolicy { UserAgentSuffix = " samples-net/3" };

        //Reusable instance of DocumentClient which represents the connection to a DocumentDB endpoint
        private static DocumentClient client;

        public static void Main(string[] args)
        {
            try
            {
                //Get a single instance of Document client and reuse this for all the samples
                //This is the recommended approach for DocumentClient as opposed to new'ing up new instances each time
                using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
                {
                    //ensure the database & collection exist before running samples
                    Init();

                    RunDocumentsDemo().Wait();

                    //Clean-up environment
                    Cleanup();
                }
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("\nEnd of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        private static async Task RunDocumentsDemo()
        {
            await UseDynamics();
        }
        
        /// <summary>
        /// 2. Basic CRUD operations using dynamics instead of POCOs
        /// </summary>
        private static async Task UseDynamics()
        {
            Console.WriteLine("\n2. Use Dynamics");

            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

            //Create a dynamic object,
            //Notice the case here. The properties will be created in the database
            //with whatever case you give the property
            //If you read this back in to a Document object id will get mapped to Id
            dynamic dynamicOrder = new
            {
                id = "DYN01",
                purchaseOrderNumber = "PO18009186470",
                orderDate = DateTime.UtcNow,
                total = 5.95,
            };

            Console.WriteLine("\nCreating document");

            ResourceResponse<Document> response = await client.CreateDocumentAsync(collectionLink, dynamicOrder);
            var createdDocument = response.Resource;

            Console.WriteLine("Document with id {0} created", createdDocument.Id);
            Console.WriteLine("Request charge of operation: {0}", response.RequestCharge);

            response = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, "DYN01"));
            var readDocument = response.Resource;

            //update a dynamic object by just creating a new Property on the fly
            //Document is itself a dynamic object, so you can just use this directly too if you prefer
            readDocument.SetPropertyValue("shippedDate", DateTime.UtcNow);

            //if you wish to work with a dynamic object so you don't need to use SetPropertyValue() or GetPropertyValue<T>()
            //then you can cast to a dynamic
            dynamicOrder = (dynamic)readDocument;
            dynamicOrder.foo = "bar";

            //now do a replace using this dynamic document
            //notice here you don't have to set collectionLink, or documentSelfLink, 
            //everything that is needed is contained in the readDynOrder object 
            //it has a .self Property
            Console.WriteLine("\nReplacing document");

            response = await client.ReplaceDocumentAsync(dynamicOrder);
            var replaced = response.Resource;

            Console.WriteLine("Request charge of operation: {0}", response.RequestCharge);
            Console.WriteLine("shippedDate: {0} and foo: {1} of replaced document", replaced.GetPropertyValue<DateTime>("shippedDate"), replaced.GetPropertyValue<string>("foo"));
        }

        private static void Cleanup()
        {
            client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId)).Wait();
        }
#endif

        public static void Init(DocumentClient client, string databaseId, string collectionId)
        {
            GetOrCreateDatabaseAsync(client, databaseId).Wait();
            GetOrCreateCollectionAsync(client, databaseId, collectionId).Wait();
        }

        private static async Task<DocumentCollection> GetOrCreateCollectionAsync(DocumentClient client, string databaseId, string collectionId)
        {
            var databaseUri = UriFactory.CreateDatabaseUri(databaseId);

            DocumentCollection collection = client.CreateDocumentCollectionQuery(databaseUri)
                .Where(c => c.Id == collectionId)
                .AsEnumerable()
                .FirstOrDefault();

            if (collection == null)
            {
                collection = await client.CreateDocumentCollectionAsync(databaseUri, new DocumentCollection { Id = collectionId });
            }

            return collection;
        }

        private static async Task<Database> GetOrCreateDatabaseAsync(DocumentClient client, string databaseId)
        {
            var databaseUri = UriFactory.CreateDatabaseUri(databaseId);

            Database database = client.CreateDatabaseQuery()
                .Where(db => db.Id == databaseId)
                .ToArray()
                .FirstOrDefault();

            if (database == null)
            {
                database = await client.CreateDatabaseAsync(new Database { Id = databaseId });
            }

            return database;
        }
    }
}

