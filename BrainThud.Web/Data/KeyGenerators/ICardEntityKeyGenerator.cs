namespace BrainThud.Web.Data.KeyGenerators
{
    public interface ICardEntityKeyGenerator : ITableStorageKeyGenerator
    {
        int GeneratedUserId { get; }
        int GeneratedEntityId { get; }
        string GetRowKey(int entityId);
    }
}