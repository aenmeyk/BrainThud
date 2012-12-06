using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Linq;
using BrainThud.Web.Extensions;

namespace BrainThud.Web.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private IEnumerable<Claim> claims { get { return FederatedAuthentication.SessionAuthenticationModule.ContextSessionSecurityToken.ClaimsPrincipal.Claims; } }
        public string IdentityProvider { get { return claims.First(x => x.Type == @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider").Value.GenerateSlug(); } }
        public string NameIdentifier { get { return claims.First(x => x.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.GenerateSlug(); } }

        public void SignOut()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
        }
    }
}