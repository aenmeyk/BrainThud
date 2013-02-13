using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using BrainThud.Web.Authentication;
using BrainThud.Web.Helpers;


// TODO: Create tests
namespace BrainThud.Web.Controllers
{
//    public class AuthenticationToken
//    {
//        public AuthenticationToken()
//        {
//            Cookies =new Dictionary<string, string>();
//        }
//
//        public IDictionary<string, string> Cookies { get; private set; }
//    }
//
//    public class FederationCallbackController : ApiController
//    {
//        private static readonly object locker = new object();
//        private static readonly IDictionary<string, AuthenticationToken> tokens = new Dictionary<string, AuthenticationToken>();
//        private readonly IAuthenticationHelper authenticationHelper;
//
//        public FederationCallbackController(IAuthenticationHelper authenticationHelper)
//        {
//            this.authenticationHelper = authenticationHelper;
//        }
//
//        public HttpResponseMessage Post()
//        {
//            var cookies = HttpContext.Current.Request.Cookies;
//            var authenticationToken = new AuthenticationToken();
//
//            foreach(var key in cookies.AllKeys.Where(x => x.StartsWith("FedAuth")))
//            {
//                if(cookies[key] != null && !authenticationToken.Cookies.ContainsKey(key))
//                {
//                    authenticationToken.Cookies.Add(key, cookies[key].Value);
//                }
//            }
//
//            if (authenticationToken.Cookies.Count == 0) return new HttpResponseMessage(HttpStatusCode.BadRequest);
//
//            var response = this.Request.CreateResponse(HttpStatusCode.Redirect);
//            var nameIdentifier = this.authenticationHelper.NameIdentifier;
//
//            lock (locker)
//            {
//                if (tokens.ContainsKey(nameIdentifier)) tokens.Remove(nameIdentifier);
//                tokens.Add(nameIdentifier, authenticationToken);
//            }
//
//            response.Headers.Add("Location", "/api/federationcallback/end?nameidentifier=" + nameIdentifier);
//
//            return response;
//        }
//
//        public HttpResponseMessage Get(string id)
//        {
//            var response = this.Request.CreateResponse();
//
//            lock (locker)
//            {
//                if (!string.IsNullOrEmpty(id) &&
//                      !id.Equals("end", StringComparison.OrdinalIgnoreCase) &&
//                      tokens.ContainsKey(id))
//                {
//                    var content = string.Empty;
//
//                    foreach(var token in tokens[id].Cookies)
//                    {
//                         content += string.Format("{0}={1};", token.Key, token.Value);
//                    }
//
//                    response.Content = new StringContent(content);
//                    tokens.Remove(id);
//                }
//            }
//
//            return response;
//        }
//    }

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
