using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public interface IUserConfigurationRepository : ICardEntityRepository<UserConfiguration>
    {
        UserConfiguration GetByNameIdentifier();
    }
}