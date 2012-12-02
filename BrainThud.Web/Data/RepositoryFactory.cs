using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data
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
            // TODO: Don't hardcode entity set name
            var entitySetName = new EntitySetDictionary()[typeof(T)];

            var tableStorageContext = new TableStorageContext(this.cloudStorageServices, entitySetName);
            var keyGenerator = this.keyGeneratorFactory.GetTableStorageKeyGenerator<T>();
            return new TableStorageRepository<T>(tableStorageContext, keyGenerator);
        }
    }
}