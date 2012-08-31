using BrainThud.Model;

namespace BrainThud.Data
{
    public class UnitOfWork 
    {
        public IRepository<Nugget> Nuggets { get; set; }
    }
}