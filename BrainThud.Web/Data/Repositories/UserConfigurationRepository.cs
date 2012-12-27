using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class UserConfigurationRepository : CardEntityRepository <UserConfiguration>, IUserConfigurationRepository
    {
        private readonly IUserHelper userHelper;

        public UserConfigurationRepository(
            ITableStorageContext tableStorageContext,
            ICardEntityKeyGenerator cardKeyGenerator, 
            IUserHelper userHelper,
            string nameIdentifier)
            : base(tableStorageContext, cardKeyGenerator, nameIdentifier, CardRowTypes.CONFIGURATION)
        {
            this.userHelper = userHelper;
        }

        public UserConfiguration GetByNameIdentifier()
        {
            var userConfiguration = this.GetForUser().FirstOrDefault();

            if(userConfiguration == null)
            {
                userConfiguration = this.userHelper.CreateUserConfiguration();
                this.TableStorageContext.UserConfigurations.Add(userConfiguration);
                this.TableStorageContext.Commit();

            }

            return userConfiguration ?? this.userHelper.CreateUserConfiguration();
        }
    }
}