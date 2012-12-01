using BrainThud.Data.AzureTableStorage;
using BrainThud.Data.KeyGenerators;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data
{
    public class RepositoryFactory: IRepositoryFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly IKeyGeneratorFactory keyGeneratorFactory;

        public RepositoryFactory(ICloudStorageServices cloudStorageServices, IKeyGeneratorFactory keyGeneratorFactory)
        {
            this.cloudStorageServices = cloudStorageServices;
            this.keyGeneratorFactory = keyGeneratorFactory;
        }

        public ITableStorageRepository<T> CreateTableStorageRepository<T>()  where T: TableServiceEntity
        {
            var tableStorageContext = new TableStorageContext<T>(this.cloudStorageServices);
            var keyGenerator = this.keyGeneratorFactory.GetTableStorageKeyGenerator<T>();
            return new TableStorageRepository<T>(tableStorageContext, keyGenerator);
        }
    }
}