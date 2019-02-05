using Microsoft.WindowsAzure.Storage.Table;

namespace TableStorage
{
     class TransactionEntity : TableEntity
    {

        public TransactionEntity(string requestid,string rowkey)
        {
            this.PartitionKey = requestid;
            this.PartitionKey = rowkey;
        }

        public TransactionEntity() { }
        
        public string Event { get; set; }
        public string Application { get; set; }
        public string MessageType { get; set; }
        public string MessageString { get; set; }
        public int EventId { get; set; }
        public string ExactTargetId { get; set; }
        public string RequestType { get; set; }
    }
}
