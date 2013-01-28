using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Controllers
{
    public class FederationCallbackController : ApiController
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public FederationCallbackController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        public HttpResponseMessage Post()
        {
            var response = this.Request.CreateResponse(HttpStatusCode.Redirect);
            var nameIdentifier = this.authenticationHelper.NameIdentifier;
            response.Headers.Add("Location", "/api/federationcallback/end?nameidentifier=" + nameIdentifier);

            return response;
        }

        public string Get()
        {
            return string.Empty;
        }
    }
}
