using BrainThud.Web.Calendars;
using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContextFactory : ITableStorageContextFactory
    {
        private readonly ICloudStorageServices cloudStorageServices;
        private readonly ICardEntityKeyGenerator cardKeyGenerator;
        private readonly ICardEntityKeyGenerator quizResultKeyGenerator;
        private readonly IQuizCalendar quizCalendar;

        public TableStorageContextFactory(
            ICloudStorageServices cloudStorageServices,
            ICardEntityKeyGenerator cardKeyGenerator,
            ICardEntityKeyGenerator quizResultKeyGenerator,
            IQuizCalendar quizCalendar)
        {
            this.cloudStorageServices = cloudStorageServices;
            this.cardKeyGenerator = cardKeyGenerator;
            this.quizResultKeyGenerator = quizResultKeyGenerator;
            this.quizCalendar = quizCalendar;
        }

        public ITableStorageContext CreateTableStorageContext(string tableName, string nameIdentifier = NameIdentifiers.MASTER)
        {
            return new TableStorageContext(
                this.cloudStorageServices,
                this.cardKeyGenerator,
                this.quizResultKeyGenerator,
                this.quizCalendar,
                tableName, 
                nameIdentifier);
        }
    }
}