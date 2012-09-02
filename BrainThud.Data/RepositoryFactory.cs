using BrainThud.Data.AzureTableStorage;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data
{
    public class RepositoryFactory: IRepositoryFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly ITableStorageKeyGenerator keyGenerator;

        public RepositoryFactory(ICloudStorageServices cloudStorageServices, ITableStorageKeyGenerator keyGenerator)
        {
            this.cloudStorageServices = cloudStorageServices;
            this.keyGenerator = keyGenerator;
        }

        public ITableStorageRepository<T> CreateTableStorageRepository<T>()  where T: TableServiceEntity
        {
            // Use the TableServiceEntity name as the EntitySetName by convention
            var entitySetName = typeof(T).Name;
            var tableStorageContext = new TableStorageContext<T>(entitySetName, this.cloudStorageServices);

            return new TableStorageRepository<T>(tableStorageContext, this.keyGenerator);
        }
    }
}