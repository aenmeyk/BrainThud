using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly ICardEntityKeyGenerator cardKeyGenerator;
        private readonly ICardEntityKeyGenerator quizResultKeyGenerator;

        public TableStorageContextFactory(
            ICloudStorageServices cloudStorageServices,
            ICardEntityKeyGenerator cardKeyGenerator,
            ICardEntityKeyGenerator quizResultKeyGenerator)
        {
            this.cloudStorageServices = cloudStorageServices;
            this.cardKeyGenerator = cardKeyGenerator;
            this.quizResultKeyGenerator = quizResultKeyGenerator;
        }

        public ITableStorageContext CreateTableStorageContext(string tableName, string nameIdentifier = NameIdentifiers.MASTER)
        {
            return new TableStorageContext(
                this.cloudStorageServices,
                this.cardKeyGenerator,
                this.quizResultKeyGenerator,
                tableName, 
                nameIdentifier);
        }
    }
}