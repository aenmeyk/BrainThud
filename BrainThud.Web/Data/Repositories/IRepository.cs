using System.Linq;

namespace BrainThud.Web.Data.Repositories
{
    public interface IRepository<T>
    {
        void Update(T entity);
        T Get(string partitionKey, string rowKey);
        IQueryable<T> Get(string partitionKey);
        IQueryable<T> GetAll();
    }
}