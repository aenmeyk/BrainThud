namespace BrainThud.Web.Data.KeyGenerators
{
    public interface ICardEntityKeyGenerator : ITableStorageKeyGenerator
    {
        int GeneratedEntityId { get; }
        string GetRowKey(int entityId);
        string GetPartitionKey(int userId);
    }
}