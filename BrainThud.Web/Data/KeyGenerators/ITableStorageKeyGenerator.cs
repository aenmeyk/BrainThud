namespace BrainThud.Web.Data.KeyGenerators
{
    public interface ITableStorageKeyGenerator 
    {
        string GeneratePartitionKey(int userId);
        string GenerateRowKey();
    }
}