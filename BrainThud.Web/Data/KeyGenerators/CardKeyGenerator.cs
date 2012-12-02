using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class CardKeyGenerator : ITableStorageKeyGenerator
    {
//        private readonly ITableStorageRepository<Configuration> repository;
        private readonly IAuthenticationHelper authenticationHelper;

        public CardKeyGenerator(IAuthenticationHelper authenticationHelper)
        {
//            this.repository = repository;
            this.authenticationHelper = authenticationHelper;
        }

        public string GenerateRowKey()
        {
            // TODO: handle error if timestamp has changed.  i.e. the ID may already have been incremented.
//            var configuration = this.repository.Get(this.authenticationHelper.NameIdentifier, EntityNames.CONFIGURATION);
//            var cardId = ++configuration.LastUsedId;

//            // TODO: look into updating the user as part of a unit of work transaction
//            this.userRepository.Update(user);
//            this.userRepository.Commit();

//            return cardId.ToString();


            return "Test";
        }

        public string GeneratePartitionKey()
        {
            return this.authenticationHelper.NameIdentifier;
        }
    }
}