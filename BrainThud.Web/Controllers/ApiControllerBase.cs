using System.Web.Http;

namespace BrainThud.Web.Controllers
{
    public class ApiControllerBase : ApiController
    {
        // Allows Url.Link to be faked for testing
        public virtual string GetLink(string routeName, object routeValues)
        {
            return Url.Link(routeName, routeValues);
        }
    }
}