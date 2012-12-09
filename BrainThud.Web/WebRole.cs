using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.DependencyResolution;
using Microsoft.WindowsAzure.ServiceRuntime;
using StructureMap;

namespace BrainThud.Web
{
    public class WebRole : RoleEntryPoint
    {
        // Public to allow this to be set to a mock for testing
        public IContainer IoCContainer { get; set; }

        public WebRole()
        {
            this.IoCContainer = IoC.Initialize();
        }

        public override bool OnStart()
        {
            var cloudStorageServices = IoCContainer.GetInstance<ICloudStorageServices>();
            cloudStorageServices.SetConfigurationSettingPublisher();
            cloudStorageServices.CreateTablesIfNotCreated();
            cloudStorageServices.CreateQueusIfNotCreated();

            return base.OnStart();
        }
    }
}
