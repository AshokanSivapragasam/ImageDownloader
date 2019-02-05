using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            BrowseTableStorage();
        }

        public static void BrowseTableStorage()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=interchangeapistorage;AccountKey=dqBBaXfZdd6Ddjk9sEyQ9JD9TukyfuJn1FaryIok66yGVJUfnYZ0Kn7NahBsSqHU6CcMfCfyxy7lBRdM9rI/Zg==");

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("TransactionalLog201711");
            
            var encryptedFilePath = @"\\AZEIPRDAP01\EISuppressionFolder\FileProcessingStore\";
            var partitionKeysList = "a6097435-dbd4-4506-8463-572aceb28b3b,d4de568e-69d6-41d5-9c21-95913e2ad4e8,a84b17e7-fac1-4dca-80cb-de9e9b5a60f4,b23c2670-74a9-4cfb-b172-73e3028e5b42,2df63096-90f3-4819-a9ef-a72086de58fe,6ff3a7e0-290c-4f0a-b2c2-e30ac256e7fc,6c6d694c-8165-49e6-8381-994e0e2a2218,ac4cfa39-82b7-432e-ac8b-738d17715141,b36a47b0-2fb4-42c3-ac60-2e3f76c50651,2ee8b302-3275-4b16-a079-67edca653b81,959b86a6-ef7e-4d5b-9b51-07a4a45e7029,f27e7f71-6333-46bc-b0f4-1bea88a84932,7d2c1ab7-868b-4ea7-84aa-73a4267b8e0a,278acd63-a2e9-4fe0-8366-9983b4f3df2e,4ee5c336-1d2b-441e-a690-a5fe12324041,5170dc44-f231-4d3d-ac13-0b4357a6b70b,ed2cff10-81cb-4b16-8381-04fd881188af,d9c8112f-c709-4a4d-a07b-00527a81daa3,7e6bae8a-7c48-46a9-9d4c-23bd712ecc8d,39bb1e8f-b73b-4110-a03f-6f38913d77bc,c5081c20-840b-4a45-a61c-b89a9e40ac41,f89fc203-329b-4506-93dd-71aa02d58406,d22825a8-c840-424d-b30b-15f5d1927ddd,da1e1c79-d591-4c84-96e2-99a67a70f878,9fb692ee-3e32-44d7-80b4-6efcf7e78dd1,d6ce70fd-c56f-4a0f-a4aa-779ed46c993b,38fef6f5-7b1d-46b5-b80f-a061df842e20,8579ddcc-3815-4da6-98fb-aafc72593267,61d98bb9-3fc4-440a-9470-86cbec4cf363,52acda2b-9fcb-47d0-afe1-909948014764,06712d5d-c716-42da-9306-fce9b0db2497";
            var partitionKeys = partitionKeysList.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var partitionKey in partitionKeys)
            {
                // Create the table query.
                TableQuery<TransactionEntity> rangeQuery = new TableQuery<TransactionEntity>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

                // Loop through the results, displaying information about the entity.
                TransactionEntity entity = table.ExecuteQuery(rangeQuery).Where(r => r.MessageString.Contains("EIINTERNALFSPROD")).FirstOrDefault();

                if (entity != null)
                    File.AppendAllText(@"D:\_files\resubmittablefiles.tsv", string.Format("{0}\t{1}\t{2}\t{3}{0}.aes\n", partitionKey, entity.RequestType, entity.MessageString.Replace(" has been deleted", string.Empty), encryptedFilePath));
            }

            Console.WriteLine("Completed!");
        }
    }
}
