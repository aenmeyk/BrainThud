using BrainThud.Core;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ITableStorageContextFactory 
    {
        ITableStorageContext CreateTableStorageContext(string tableName, string nameIdentifier = NameIdentifiers.MASTER);
    }
}