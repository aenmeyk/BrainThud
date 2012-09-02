using System.Collections.Generic;

namespace BrainThud.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
    }
}