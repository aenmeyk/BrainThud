
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        private readonly ITableStorageContext<T> tableStorageContext;

        public TableStorageRepository(ITableStorageContext<T> tableStorageContext)
        {
            this.tableStorageContext = tableStorageContext;
        }

        public void Add(T entity)
        {
            this.tableStorageContext.AddObject(entity);
        }

        public void Commit()
        {
            this.tableStorageContext.SaveChangesWithRetries();
        }
    }
}