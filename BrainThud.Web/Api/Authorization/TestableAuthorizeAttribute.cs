using System.Linq;
using System.Web.Http;
using BrainThud.Core;

namespace BrainThud.Web.Api.Authorization
{
    public class TestableAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
#if DEBUG
            if (actionContext.Request.RequestUri.Host == "localhost" && 
                actionContext.Request.RequestUri.Port == 80)
            {
                var headers = actionContext.Request.Headers;
                if(headers.Contains(HttpHeaders.X_TEST) && headers.First(x => x.Key == HttpHeaders.X_TEST).Value.First() == "true")
                {
                    return true;
                }
            }
#endif

            return base.IsAuthorized(actionContext);
        }
    }
}