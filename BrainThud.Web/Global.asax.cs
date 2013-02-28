using System;
using System.IdentityModel.Services;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BrainThud.Core;
using BrainThud.Web.Api;
using BrainThud.Web.App_Start;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using StructureMap;

namespace BrainThud.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Configure(GlobalConfiguration.Configuration);
            GlobalConfig.CustomizeConfig(GlobalConfiguration.Configuration);
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FederatedAuthentication.FederationConfigurationCreated += (s, e) =>
            {
                FederatedAuthentication.WSFederationAuthenticationModule.SignedIn += WSFederationAuthenticationModule_SignedIn;
                FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenReceived += SessionAuthenticationModule_SessionSecurityTokenReceived;
            };
        }

        private void WSFederationAuthenticationModule_SignedIn(object sender, System.EventArgs e)
        {
            var container = ObjectFactory.Container;
            var tableStorageContextFactory = container.GetInstance<ITableStorageContextFactory>();
            var authenticationHelper = container.GetInstance<IAuthenticationHelper>();
            var tableStorageContext = tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, authenticationHelper.NameIdentifier);

            if (tableStorageContext.UserConfigurations.GetByNameIdentifier() == null)
            {
                var userHelper = container.GetInstance<IUserHelper>();
                var userConfiguration = userHelper.CreateUserConfiguration();
                tableStorageContext.UserConfigurations.Add(userConfiguration);
                tableStorageContext.Commit();
            }
        }

        private void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            var now = DateTime.UtcNow;
            var validFrom = e.SessionToken.ValidFrom;
            var validTo = e.SessionToken.ValidTo;

            // If the user is in the second half of their session
            if (now < validTo && now > validFrom.Add(new TimeSpan((validTo.Ticks - validFrom.Ticks) / 2)))
            {
                var module = (SessionAuthenticationModule)sender;
                e.SessionToken = module.CreateSessionSecurityToken(e.SessionToken.ClaimsPrincipal, e.SessionToken.Context,
                now, now.AddMinutes(ConfigurationSettings.SESSION_TOKEN_REISSUE_DURATION_MINUTES), e.SessionToken.IsPersistent);
                e.ReissueCookie = true;
            }
        }
    }
}