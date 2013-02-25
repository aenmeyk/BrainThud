using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Core;
using BrainThud.Web.Authentication;

namespace BrainThud.Web.Api.Controllers
{
    public class FederationCallbackController : ApiController
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ITokenStore tokenStore;

        public FederationCallbackController(IAuthenticationHelper authenticationHelper, ITokenStore tokenStore)
        {
            this.authenticationHelper = authenticationHelper;
            this.tokenStore = tokenStore;
        }

        public HttpResponseMessage Post()
        {
            var nameIdentifier = this.authenticationHelper.NameIdentifier;
            var cookieCollection = this.Request.Headers.GetCookies().FirstOrDefault();

            if (cookieCollection == null || cookieCollection.Cookies.Count == 0) return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var cookies = cookieCollection.Cookies;

            foreach(var cookie in cookies.Where(x => x.Name.StartsWith("FedAuth")))
            {
                this.tokenStore.AddTokenCookie(nameIdentifier, cookie.Name, cookie.Value);
            }

            var response = this.Request.CreateResponse(HttpStatusCode.Redirect);
            var locationUrl = string.Format("{0}{1}?nameidentifier={2}", Routes.FEDERATION_CALLBACK, Constants.FEDERATION_CALLBACK_END_ID, nameIdentifier);
            response.Headers.Add("Location", locationUrl);

            return response;
        }

        // TODO: Break this into two endpoints: one for the POST redirect and one to get the token
        public HttpResponseMessage Get(string id)
        {
            var response = this.Request.CreateResponse();

            // This method is called with an id equal to Constants.FEDERATION_CALLBACK_END_ID as a result of the redirect
            // in FederationCallbackController.Post.  The WinRT calling app waits for this URL before returning control
            // back to the client.  We don't want to get the token in that special case.
            if (!id.Equals(Constants.FEDERATION_CALLBACK_END_ID, StringComparison.OrdinalIgnoreCase))
            {
                var token = this.tokenStore.GetAndDeleteToken(id);
                response.Content = new StringContent(token);
            }

            return response;
        }
    }
}
