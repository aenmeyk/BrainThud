namespace BrainThud.Data.AzureTableStorage
{
    public interface ITableStorageKeyGenerator 
    {
        string GeneratePartitionKey();
        string GenerateRowKey();
    }
}