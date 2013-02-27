using System.IdentityModel.Services;

namespace BrainThud.Web.Authentication
{
    public interface IAuthenticationHelper 
    {
        string IdentityProvider { get; }
        string NameIdentifier { get; }
        string SignOut();
    }
}