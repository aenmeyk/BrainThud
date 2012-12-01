using System;

namespace BrainThud.Data.KeyGenerators
{
    public class TableStorageKeyGenerator : ITableStorageKeyGenerator
    {
        public string GeneratePartitionKey()
        {
            // TODO: Use some sort of subject or category as the partition key
            return Keys.TEMP_PARTITION_KEY;
        }

        public string GenerateRowKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}