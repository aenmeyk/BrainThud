namespace BrainThud.Web.Authentication
{
    public interface ITokenStore 
    {
        void AddTokenCookie(string nameIdentifier, string key, string value);
        string GetAndDeleteToken(string nameIdentifier);
    }
}