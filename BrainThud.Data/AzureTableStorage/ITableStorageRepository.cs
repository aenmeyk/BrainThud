namespace BrainThud.Data.AzureTableStorage
{
    public interface ITableStorageRepository<T>
    {
        void Commit();
    }
}