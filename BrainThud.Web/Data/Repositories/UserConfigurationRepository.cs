using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class UserConfigurationRepository : CardEntityRepository <UserConfiguration>, IUserConfigurationRepository
    {
        public UserConfigurationRepository(
            ITableStorageContext tableStorageContext,
            ICardEntityKeyGenerator cardKeyGenerator, 
            string nameIdentifier)
            : base(tableStorageContext, cardKeyGenerator, nameIdentifier, CardRowTypes.CONFIGURATION){}

        public UserConfiguration GetByNameIdentifier()
        {
            return this.GetForUser().FirstOrDefault();
        }
    }
}