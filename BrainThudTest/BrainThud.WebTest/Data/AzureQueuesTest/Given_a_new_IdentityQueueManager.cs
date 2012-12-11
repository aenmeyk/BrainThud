﻿using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;
using BrainThudTest.BrainThud.WebTest.Fakes;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureQueuesTest
{
    public abstract class Given_a_new_IdentityQueueManager : Gwt
    {
        protected const int CURRENT_MAX_IDENTITY = 1000;

        public override void Given()
        {
            // Set the refresh interval to 0 so that the test thread doesn't sleep
            ConfigurationSettings.SeedRefreshIntervalSeconds = 0;

            this.CloudStorageServices = new Mock<ICloudStorageServices> { DefaultValue = DefaultValue.Mock };
            this.TableStorageContext = new Mock<ITableStorageContext>{ DefaultValue = DefaultValue.Mock };

            this.MasterConfiguration = new MasterConfiguration{CurrentMaxIdentity = CURRENT_MAX_IDENTITY};
            this.TableStorageContext.Setup(x => x.MasterConfigurations.GetOrCreate(Keys.MASTER, Keys.CONFIGURATION)).Returns(this.MasterConfiguration);

            var tableStorageContextFactory = new Mock<ITableStorageContextFactory>();
            tableStorageContextFactory
                .Setup(x => x.CreateTableStorageContext(AzureTableNames.CONFIGURATION, NameIdentifiers.MASTER))
                .Returns(this.TableStorageContext.Object);

            this.IdentityQueueManager = new FakeIdentityQueueManager(tableStorageContextFactory.Object, this.CloudStorageServices.Object);
        }

        protected Mock<ICloudStorageServices> CloudStorageServices { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected MasterConfiguration MasterConfiguration { get; private set; }
        protected FakeIdentityQueueManager IdentityQueueManager { get; private set; }
    }
}