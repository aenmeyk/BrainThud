
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;

        public TableStorageContextFactory(ICloudStorageServices cloudStorageServices)
        {
            this.cloudStorageServices = cloudStorageServices;
        }

        public ITableStorageContext CreateTableStorageContext(string entitySetName)
        {
            return new TableStorageContext(this.cloudStorageServices, entitySetName);
        }
    }
}