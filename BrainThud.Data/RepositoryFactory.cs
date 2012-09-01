using BrainThud.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data
{
    public class RepositoryFactory: IRepositoryFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;

        public RepositoryFactory(ICloudStorageServices cloudStorageServices)
        {
            this.cloudStorageServices = cloudStorageServices;
        }

        public ITableStorageRepository<T> CreateTableStorageRepository<T>()  where T: TableServiceEntity
        {
            // Use the TableServiceEntity name as the EntitySetName by convention
            var entitySetName = typeof(T).Name;
            var tableStorageContext = new TableStorageContext<T>(entitySetName, this.cloudStorageServices);

            return new TableStorageRepository<T>(tableStorageContext);
        }
    }
}