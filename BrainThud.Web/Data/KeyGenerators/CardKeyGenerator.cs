using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.KeyGenerators
{
//    public class CardKeyGenerator : ITableStorageKeyGenerator
//    {
//        public string GeneratePartitionKey()
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public string GenerateRowKey()
//        {
//            throw new System.NotImplementedException();
//        }
//    }

    public class CardKeyGenerator : ITableStorageKeyGenerator
    {
        private readonly ITableStorageRepository<Configuration> repository;
        private readonly IAuthenticationHelper authenticationHelper;

        public CardKeyGenerator(ITableStorageRepository<Configuration> repository, IAuthenticationHelper authenticationHelper)
        {
            this.repository = repository;
            this.authenticationHelper = authenticationHelper;
        }

        public string GenerateRowKey()
        {
            // TODO: handle error if timestamp has changed.  i.e. the ID may already have been incremented.
            var configuration = this.repository.Get(this.authenticationHelper.NameIdentifier, EntityNames.CONFIGURATION);
            var cardId = ++configuration.LastUsedId;

//            // TODO: look into updating the user as part of a unit of work transaction
//            this.userRepository.Update(user);
//            this.userRepository.Commit();

            return cardId.ToString();

        }

        public string GeneratePartitionKey()
        {
            return this.authenticationHelper.NameIdentifier;
        }
    }

//    public class CardKeyGenerator 
//    {
//        private readonly string userRowKey;
//        private readonly ITableStorageRepository<User> userRepository;
//
//        public CardKeyGenerator(IRepositoryFactory repositoryFactory, string userRowKey)
//        {
//            this.userRowKey = userRowKey;
//            this.userRepository = repositoryFactory.CreateTableStorageRepository<User>();
//        }
//
//        public string GenerateRowKey()
//        {
//            // TODO: handle error if timestamp has changed.  i.e. the ID may already have been incremented.
//            var user = this.userRepository.Get(this.userRowKey);
//            var cardId = ++user.LastUsedCardId;
//
//            // TODO: look into updating the user as part of a unit of work transaction
//            this.userRepository.Update(user);
//            this.userRepository.Commit();
//
//            return string.Format("{0}_{1}_{2}", CardRowTypes.CARD, cardId, user.Id);
//        }
//
//        public string GeneratePartitionKey()
//        {
//            return this.userRowKey;
//        }
//    }
}