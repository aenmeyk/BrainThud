namespace BrainThud.Web.Helpers
{
    public interface IAuthenticationHelper 
    {
        string IdentityProvider { get; }
        string NameIdentifier { get; }
        void SignOut();
    }
}