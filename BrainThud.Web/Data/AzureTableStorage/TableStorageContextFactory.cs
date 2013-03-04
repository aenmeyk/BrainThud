using BrainThud.Core;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly ICardEntityKeyGenerator cardKeyGenerator;
        private readonly ICardEntityKeyGenerator cardDeckKeyGenerator;
        private readonly ICardEntityKeyGenerator quizResultKeyGenerator;
        private readonly ICardEntityKeyGenerator userConfigurationKeyGenerator;
        private readonly IRepositoryFactory repositoryFactory;

        public TableStorageContextFactory(
            ICloudStorageServices cloudStorageServices,
            ICardEntityKeyGenerator cardKeyGenerator,
            ICardEntityKeyGenerator cardDeckKeyGenerator,
            ICardEntityKeyGenerator quizResultKeyGenerator,
            ICardEntityKeyGenerator userConfigurationKeyGenerator,
            IRepositoryFactory repositoryFactory)
        {
            this.cardKeyGenerator = cardKeyGenerator;
            this.cardDeckKeyGenerator = cardDeckKeyGenerator;
            this.cloudStorageServices = cloudStorageServices;
            this.quizResultKeyGenerator = quizResultKeyGenerator;
            this.userConfigurationKeyGenerator = userConfigurationKeyGenerator;
            this.repositoryFactory = repositoryFactory;
        }

        public ITableStorageContext CreateTableStorageContext(string tableName, string nameIdentifier = NameIdentifiers.MASTER)
        {
            return new TableStorageContext(
                this.cloudStorageServices,
                this.cardKeyGenerator,
                this.cardDeckKeyGenerator,
                this.quizResultKeyGenerator,
                this.userConfigurationKeyGenerator,
                this.repositoryFactory,
                tableName, 
                nameIdentifier);
        }
    }
}