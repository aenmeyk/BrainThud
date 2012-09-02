using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;

namespace BrainThud.Data
{
    public interface IUnitOfWork 
    {
        ITableStorageRepository<Nugget> Nuggets { get; }
        void Commit();
    }
}