using System.Collections.Generic;

namespace BrainThud.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        T Get(string rowKey);
        IEnumerable<T> GetAll();
    }
}