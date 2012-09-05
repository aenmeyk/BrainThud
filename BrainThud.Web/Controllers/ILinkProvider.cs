namespace BrainThud.Web.Controllers
{
    public interface ILinkProvider
    {
        string GetLink(string routeName, object routeValues);
    }
}