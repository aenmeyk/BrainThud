using System;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class TableStorageKeyGenerator : ITableStorageKeyGenerator
    {
        public string GeneratePartitionKey()
        {
            // TODO: Use some sort of meaningfule value as the partition key
            return "TEMP_PARTITION_KEY";
        }

        public string GenerateRowKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}