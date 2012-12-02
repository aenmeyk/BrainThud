namespace BrainThud.Web.Data.KeyGenerators
{
    public interface ITableStorageKeyGenerator 
    {
        string GeneratePartitionKey();
        string GenerateRowKey();
    }
}