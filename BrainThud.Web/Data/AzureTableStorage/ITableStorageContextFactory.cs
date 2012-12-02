namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ITableStorageContextFactory 
    {
        ITableStorageContext CreateTableStorageContext(string entitySetName);
    }
}