using System.Web.Http;
using BrainThud.Web.Api.Authorization;

namespace BrainThud.Web.Api.Controllers
{
    [TestableAuthorize]
    public class ApiControllerBase : ApiController
    {
        // Allows Url.Link to be faked for testing
        public virtual string GetLink(string routeName, object routeValues)
        {
            return this.Url.Link(routeName, routeValues);
        }
    }
}