using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly IKeyGeneratorFactory keyGeneratorFactory;

        public TableStorageContextFactory(ICloudStorageServices cloudStorageServices, IKeyGeneratorFactory keyGeneratorFactory)
        {
            this.cloudStorageServices = cloudStorageServices;
            this.keyGeneratorFactory = keyGeneratorFactory;
        }

        public ITableStorageContext CreateTableStorageContext(string entitySetName)
        {
            return new TableStorageContext(cloudStorageServices, this.keyGeneratorFactory, entitySetName);
        }
    }
}