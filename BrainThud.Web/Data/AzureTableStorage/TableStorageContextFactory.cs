namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;

        public TableStorageContextFactory(ICloudStorageServices cloudStorageServices)
        {
            this.cloudStorageServices = cloudStorageServices;
        }

        public ITableStorageContext CreateTableStorageContext(string tableName, string nameIdentifier = NameIdentifiers.MASTER)
        {
            return new TableStorageContext(this.cloudStorageServices, tableName, nameIdentifier);
        }
    }
}