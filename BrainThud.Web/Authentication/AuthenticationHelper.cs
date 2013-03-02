using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Net;
using System.Security.Claims;
using System.Linq;
using System.Web;
using BrainThud.Core;
using BrainThud.Web.Extensions;

namespace BrainThud.Web.Authentication
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
                    ? this.claims.First(x => x.Type == IDENTITY_PROVIDER).Value.GenerateSlug(ConfigurationSettings.PARTITION_KEY_SLUG_LENGTH)
                    : null;
            }
        }

        public string NameIdentifier
        {
            get
            {
#if DEBUG
                if (HttpContext.Current == null) return "httpswwwgooglecomaccountso8ididaitoawn66whrug-vmzp4sx7ikz2px5njx5dbv2u";
//                if (HttpContext.Current == null) return "TestNameIdentifier";
#endif

                return FederatedAuthentication.SessionAuthenticationModule.ContextSessionSecurityToken != null
                    ? this.claims.First(x => x.Type == NAMEIDENTIFIER).Value.GenerateSlug(ConfigurationSettings.PARTITION_KEY_SLUG_LENGTH)
                    : null;
            }
        }

        public string SignOut()
        {
            var module = FederatedAuthentication.WSFederationAuthenticationModule;
            module.SignOut();
            var signOutRequestMessage = new SignOutRequestMessage(new Uri(module.Issuer), module.Realm);

            return string.Format("{0}&wtrealm={1}", signOutRequestMessage.WriteQueryString(), WebUtility.UrlEncode(module.Realm));
        }
    }
}