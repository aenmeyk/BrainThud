using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Linq;
using System.Web;
using BrainThud.Web.Extensions;

namespace BrainThud.Web.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private const string IDENTITY_PROVIDER = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";
        private const string NAMEIDENTIFIER = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private IEnumerable<Claim> claims { get { return FederatedAuthentication.SessionAuthenticationModule.ContextSessionSecurityToken.ClaimsPrincipal.Claims; } }

        public string IdentityProvider
        {
            get
            {
#if DEBUG
                if (HttpContext.Current == null) return "TestIdentityProvider";
#endif
                return FederatedAuthentication.SessionAuthenticationModule.ContextSessionSecurityToken != null
                    ? this.claims.First(x => x.Type == IDENTITY_PROVIDER).Value.GenerateSlug()
                    : null;
            }
        }

        public string NameIdentifier
        {
            get
            {
#if DEBUG
                if (HttpContext.Current == null) return "TestNameIdentifier";
#endif

                return FederatedAuthentication.SessionAuthenticationModule.ContextSessionSecurityToken != null
                    ? claims.First(x => x.Type == NAMEIDENTIFIER).Value.GenerateSlug()
                    : null;
            }
        }

        public void SignOut()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
        }
    }
}