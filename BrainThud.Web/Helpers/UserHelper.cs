using System;
using System.Xml.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;

        public UserHelper(ITableStorageContextFactory tableStorageContextFactory)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
        }

        public UserConfiguration CreateUserConfiguration(string nameIdentifier)
        {
            var userId = this.GetNextId();
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);

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
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);

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
