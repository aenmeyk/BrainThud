using System;
using System.Threading;
using BrainThud.Core;
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

        public override void Run()
        {
            var cloudStorageServices = IoCContainer.GetInstance<ICloudStorageServices>();
            cloudStorageServices.CreateTablesIfNotCreated();
            cloudStorageServices.CreateQueusIfNotCreated();

            var identityQueueSeeder = this.IoCContainer.GetInstance<IIdentityQueueSeeder>();

            do
            {
                identityQueueSeeder.Seed();
                Thread.Sleep(TimeSpan.FromSeconds(ConfigurationSettings.SeedRefreshIntervalSeconds));
            } while (this.enableContinuousExecution);
        }
    }
}
