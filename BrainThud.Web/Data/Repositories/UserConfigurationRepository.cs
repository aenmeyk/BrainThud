using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class UserConfigurationRepository : CardEntityRepository <UserConfiguration>, IUserConfigurationRepository
    {
        public UserConfigurationRepository(ITableStorageContext tableStorageContext, string nameIdentifier)
            : base(tableStorageContext, nameIdentifier, CardRowTypes.CONFIGURATION) { }

        public UserConfiguration GetByNameIdentifier()
        {
            return this.GetAllForUser().FirstOrDefault();
        }
    }
}