namespace BrainThud.Web.Authentication
{
    public interface IAuthenticationHelper 
    {
        string IdentityProvider { get; }
        string NameIdentifier { get; }
        void SignOut();
    }
}