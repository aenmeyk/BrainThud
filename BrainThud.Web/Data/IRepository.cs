using System.Linq;

namespace BrainThud.Web.Data
{
    public interface IRepository<T>
    {
        void Update(T entity);
        T Get(string partitionKey, string rowKey);
        IQueryable<T> GetAll();
    }
}