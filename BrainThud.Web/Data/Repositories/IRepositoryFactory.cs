using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Data.Repositories
{
    public interface IRepositoryFactory
    {
        TReturn CreateRepository<TRepository, TReturn>(ITableStorageContext tableStorageContext, string rowType, string nameIdentifier)
            where TRepository : TableStorageRepositoryBase;
    }
}