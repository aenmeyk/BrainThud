using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public interface IUserRepository : ICardRepository<UserConfiguration>
    {
        UserConfiguration GetByNameIdentifier();
    }
}