using BrainThud.Core;
using BrainThud.Web.Data.Repositories;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly IRepositoryFactory repositoryFactory;

        public TableStorageContextFactory(
            ICloudStorageServices cloudStorageServices,
            IRepositoryFactory repositoryFactory)
        {
            this.cloudStorageServices = cloudStorageServices;
            this.repositoryFactory = repositoryFactory;
        }

        public ITableStorageContext CreateTableStorageContext(string tableName, string nameIdentifier = NameIdentifiers.MASTER)
        {
            return new TableStorageContext(
                this.cloudStorageServices,
                this.repositoryFactory,
                tableName, 
                nameIdentifier);
        }
    }
}