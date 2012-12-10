using System;
using System.Threading;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.DependencyResolution;
using Microsoft.WindowsAzure.ServiceRuntime;
using StructureMap;

namespace BrainThud.Web
{
    public class WebRole : RoleEntryPoint
    {
        private readonly bool enableContinuousExecution = true;

        // Public to allow this to be set to a mock for testing
        public IContainer IoCContainer { get; set; }

        public WebRole()
        {
            this.IoCContainer = IoC.Initialize();
        }

        public WebRole(bool enableContinuousExecution)
        {
            this.enableContinuousExecution = enableContinuousExecution;
        }

        public override bool OnStart()
        {
            var cloudStorageServices = IoCContainer.GetInstance<ICloudStorageServices>();
            cloudStorageServices.SetConfigurationSettingPublisher();
            cloudStorageServices.CreateTablesIfNotCreated();
            cloudStorageServices.CreateQueusIfNotCreated();

            var identityQueueManager = this.IoCContainer.GetInstance<IIdentityQueueManager>();

            do
            {
                identityQueueManager.Seed();
                Thread.Sleep(TimeSpan.FromSeconds(ConfigurationSettings.SeedRefreshIntervalSeconds));
            } while (this.enableContinuousExecution);

            return base.OnStart();
        }
    }
}
