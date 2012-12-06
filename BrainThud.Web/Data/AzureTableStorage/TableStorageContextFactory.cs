
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly IAuthenticationHelper authenticationHelper;

        public TableStorageContextFactory(ICloudStorageServices cloudStorageServices, IAuthenticationHelper authenticationHelper)
        {
            this.cloudStorageServices = cloudStorageServices;
            this.authenticationHelper = authenticationHelper;
        }

        public ITableStorageContext CreateTableStorageContext(string entitySetName)
        {
            return new TableStorageContext(this.cloudStorageServices, this.authenticationHelper, entitySetName);
        }
    }
}