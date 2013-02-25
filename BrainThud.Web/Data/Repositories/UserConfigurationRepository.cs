using System.Linq;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.Repositories
{
    public class UserConfigurationRepository : CardEntityRepository <UserConfiguration>, IUserConfigurationRepository
    {
        public UserConfigurationRepository(
            ITableStorageContext tableStorageContext,
            ICardEntityKeyGenerator userConfigurationKeyGenerator, 
            string nameIdentifier)
            : base(tableStorageContext, userConfigurationKeyGenerator, nameIdentifier, CardRowTypes.CONFIGURATION){}

        public UserConfiguration GetByNameIdentifier()
        {
            return this.GetForUser().FirstOrDefault();
        }

        public UserConfiguration GetByUserId(int userId)
        {
            var rowKey = this.KeyGenerator.GetRowKey(userId);
            return this.EntitySet.Where(x => x.RowKey == rowKey).FirstOrDefault();
        }
    }
}