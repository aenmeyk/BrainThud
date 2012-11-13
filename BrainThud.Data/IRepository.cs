using System.Linq;

namespace BrainThud.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        T Get(string rowKey);
        IQueryable<T> GetAll();
    }
}