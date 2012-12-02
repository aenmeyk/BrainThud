using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Linq;

namespace BrainThud.Web.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IEnumerable<Claim> claims;

        public AuthenticationHelper()
        {
            this.claims = FederatedAuthentication.SessionAuthenticationModule.ContextSessionSecurityToken.ClaimsPrincipal.Claims;
        }

        public string IdentityProvider { get { return claims.First(x => x.Type == @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider").Value; } }
        public string NameIdentifier { get { return claims.First(x => x.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value; } }

    }
}