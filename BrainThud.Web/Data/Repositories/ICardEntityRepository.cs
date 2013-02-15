using System.Linq;
using Microsoft.WindowsAzure.Storage;

namespace BrainThud.Web.Data.Repositories
{
    public interface ICardEntityRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        IQueryable<T> GetForUser();
        T GetById(int userId, int entityId);
        void DeleteById(int userId, int entityId);
    }
}