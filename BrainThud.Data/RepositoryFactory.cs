using BrainThud.Data.AzureTableStorage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data
{
    public class RepositoryFactory: IRepositoryFactory
    {
        private readonly CloudStorageAccount cloudStorageAccount;

        public RepositoryFactory(CloudStorageAccount cloudStorageAccount)
        {
//            this.cloudStorageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            this.cloudStorageAccount = cloudStorageAccount;
        }

        public ITableStorageRepository<T> CreateTableStorageRepository<T>(bool createTable = true)  where T: TableServiceEntity
        {
            // Use the TableServiceEntity name as the EntitySetName by convention
            var entitySetName = typeof(T).Name;
            var tableStorageContext = new TableStorageContext<T>(entitySetName, this.cloudStorageAccount, createTable);

            return new TableStorageRepository<T>(tableStorageContext);
        }
    }
}