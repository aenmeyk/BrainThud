using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrainThud.Core.AzureServices
{
    public interface IAccessControlService
    {
        Task<IEnumerable<IdentityProvider>> GetIdentityProviders();
    }
}