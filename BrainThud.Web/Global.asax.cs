using System.Configuration;
using System.IdentityModel.Services;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BrainThud.Web.App_Start;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using StructureMap;

namespace BrainThud.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                string connectionString;

                if (RoleEnvironment.IsAvailable)
                {
                    connectionString = RoleEnvironment.GetConfigurationSettingValue(configName);
                }
                else
                {
                    connectionString = ConfigurationManager.AppSettings[configName];
                }

                configSetter(connectionString);
            });

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Configure(GlobalConfiguration.Configuration); 
            GlobalConfig.CustomizeConfig(GlobalConfiguration.Configuration);
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FederatedAuthentication.FederationConfigurationCreated += (s, e) =>
            {
                FederatedAuthentication.WSFederationAuthenticationModule.SignedIn += WSFederationAuthenticationModule_SignedIn;
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

    }
}