using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BrainThud.Web.Helpers;


// TODO: Create tests
namespace BrainThud.Web.Controllers
{
    public class AuthenticationToken
    {
        public AuthenticationToken()
        {
            Cookies =new Dictionary<string, string>();
        }

        public IDictionary<string, string> Cookies { get; private set; }
    }

    public class FederationCallbackController : ApiController
    {
        private static readonly object locker = new object();
        private static readonly IDictionary<string, AuthenticationToken> tokens = new Dictionary<string, AuthenticationToken>();
        private readonly IAuthenticationHelper authenticationHelper;

        public FederationCallbackController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        public HttpResponseMessage Post()
        {
            var cookies = HttpContext.Current.Request.Cookies;
            var authenticationToken = new AuthenticationToken();

            foreach(var key in cookies.AllKeys.Where(x => x.StartsWith("FedAuth")))
            {
                if(cookies[key] != null && !authenticationToken.Cookies.ContainsKey(key))
                {
                    authenticationToken.Cookies.Add(key, cookies[key].Value);
                }
            }

            if (authenticationToken.Cookies.Count == 0) return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var response = this.Request.CreateResponse(HttpStatusCode.Redirect);
            var nameIdentifier = this.authenticationHelper.NameIdentifier;

            lock (locker)
            {
                if (tokens.ContainsKey(nameIdentifier)) tokens.Remove(nameIdentifier);
                tokens.Add(nameIdentifier, authenticationToken);
            }

            response.Headers.Add("Location", "/api/federationcallback/end?nameidentifier=" + nameIdentifier);

            return response;
        }

        public HttpResponseMessage Get(string id)
        {
            var response = this.Request.CreateResponse();

            lock (locker)
            {
                if (!string.IsNullOrEmpty(id) &&
                      !id.Equals("end", StringComparison.OrdinalIgnoreCase) &&
                      tokens.ContainsKey(id))
                {
                    var content = string.Empty;

                    foreach(var token in tokens[id].Cookies)
                    {
                         content += string.Format("{0}={1};", token.Key, token.Value);
                    }

                    response.Content = new StringContent(content);
                    tokens.Remove(id);
                }
            }

            return response;
        }
    }
}
