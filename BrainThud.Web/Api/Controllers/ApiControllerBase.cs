using System.Web.Http;

namespace BrainThud.Web.Api.Controllers
{
    [Authorize]
    public class ApiControllerBase : ApiController
    {
        // Allows Url.Link to be faked for testing
        public virtual string GetLink(string routeName, object routeValues)
        {
            return this.Url.Link(routeName, routeValues);
        }
    }
}