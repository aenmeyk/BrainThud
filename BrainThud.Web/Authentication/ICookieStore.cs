namespace BrainThud.Web.Authentication
{
    public interface ICookieStore 
    {
        void AddOrUpdate(string key, string value);
        string GetAndDeleteToken(string nameIdentifier);
    }
}