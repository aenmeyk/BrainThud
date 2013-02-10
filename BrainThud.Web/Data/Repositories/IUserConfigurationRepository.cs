
using BrainThud.Core.Models;

namespace BrainThud.Web.Data.Repositories
{
    public interface IUserConfigurationRepository : ICardEntityRepository<UserConfiguration>
    {
        UserConfiguration GetByNameIdentifier();
    }
}