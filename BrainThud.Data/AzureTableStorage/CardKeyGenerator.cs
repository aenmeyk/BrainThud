using BrainThud.Model;

namespace BrainThud.Data.AzureTableStorage
{
    public class CardKeyGenerator 
    {
        private readonly string userRowKey;
        private readonly ITableStorageRepository<User> userRepository;

        public CardKeyGenerator(IRepositoryFactory repositoryFactory, string userRowKey)
        {
            this.userRowKey = userRowKey;
            this.userRepository = repositoryFactory.CreateTableStorageRepository<User>();
        }

        public string GenerateRowKey()
        {
            // TODO: handle error if timestamp has changed.  i.e. the ID may already have been incremented.
            var user = this.userRepository.Get(userRowKey);
            var cardId = ++user.LastUsedCardId;

            // TODO: look into updating the user as part of a unit of work transaction
            this.userRepository.Update(user);
            this.userRepository.Commit();

            return string.Format("{0}_{1}_{2}", CardRowTypes.CARD, cardId, user.Id);
        }

        public string GeneratePartitionKey()
        {
            return this.userRowKey;
        }
    }
}