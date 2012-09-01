namespace BrainThud.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);
    }
}