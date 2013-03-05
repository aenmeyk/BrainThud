using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.Repositories
{
    public interface IRepositoryFactory {
        T CreateRepository<T>(ITableStorageContext tableStorageContext, string rowType, string nameIdentifier)
            where T : TableStorageRepositoryBase;
    }
}