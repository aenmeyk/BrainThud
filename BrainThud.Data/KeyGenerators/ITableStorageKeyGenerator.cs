namespace BrainThud.Data.KeyGenerators
{
    public interface ITableStorageKeyGenerator 
    {
        string GeneratePartitionKey();
        string GenerateRowKey();
    }
}