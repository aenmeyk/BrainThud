using System;
using System.Xml.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly string nameIdentifier;

        public UserHelper(ITableStorageContextFactory tableStorageContextFactory, string nameIdentifier)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
            this.nameIdentifier = nameIdentifier;
        }

        public UserConfiguration CreateUserConfiguration()
        {
            var userId = this.GetNextId();
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD, this.nameIdentifier);

            var configuration = new UserConfiguration
            {
                PartitionKey = string.Format("{0}-{1}", nameIdentifier, userId),
                RowKey = EntityNames.CONFIGURATION,
                UserId = userId
            };

            tableStorageContext.UserConfigurations.Add(configuration);
            tableStorageContext.Commit();

            return configuration;
        }

        private int GetNextId()
        {
            var retries = 0;
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD, this.nameIdentifier);

            while (true)
            {
                retries++;
                var masterConfiguration = tableStorageContext.MasterConfigurations.GetOrCreate(PartitionKeys.MASTER, EntityNames.CONFIGURATION);

                try
                {
                    var userId = ++masterConfiguration.LastUsedUserId;
                    tableStorageContext.MasterConfigurations.Update(masterConfiguration);
                    tableStorageContext.Commit();

                    return userId;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null && retries < ConfigurationSettings.CONCURRENCY_VIOLATION_RETRIES)
                    {
                        // TODO: Move the exception parsing into its own class
                        var errorXml = XElement.Parse(e.InnerException.Message);
                        var code = errorXml.FirstNode as XElement;
                        if (code != null && code.Value == AzureErrorCodes.CONCURRENCY_VIOLATION)
                        {
                            tableStorageContext.Detach(masterConfiguration);
                            continue;
                        }
                    }

                    throw;
                }
            }
        }
    }
}
