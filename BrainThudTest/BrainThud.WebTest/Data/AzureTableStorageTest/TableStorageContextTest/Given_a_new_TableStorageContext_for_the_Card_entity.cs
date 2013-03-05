using BrainThud.Core;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.Repositories;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageContext_for_the_Card_entity : Gwt
    {
        public override void Given()
        {
            this.CloudStorageServices = new MockCloudStorageServicesBuilder().Build();

            this.CardRepository = new Mock<ICardRepository>();
            this.CardDeckRepository = new Mock<ICardDeckRepository>();
            this.QuizResultsRepository = new Mock<IQuizResultsRepository>();
            this.UserConfigurationRepository = new Mock<IUserConfigurationRepository>();

            this.RepositoryFactory = new Mock<IRepositoryFactory>();

            this.RepositoryFactory.Setup(x => x.CreateRepository<CardRepository, ICardRepository>(
                It.IsAny<ITableStorageContext>(), CardRowTypes.CARD, TestValues.NAME_IDENTIFIER))
                .Returns(this.CardRepository.Object);

            this.RepositoryFactory.Setup(x => x.CreateRepository<CardDeckRepository, ICardDeckRepository>(
                It.IsAny<ITableStorageContext>(), CardRowTypes.CARD_DECK, TestValues.NAME_IDENTIFIER))
                .Returns(this.CardDeckRepository.Object);

            this.RepositoryFactory.Setup(x => x.CreateRepository<QuizResultsRepository, IQuizResultsRepository>(
                It.IsAny<ITableStorageContext>(), CardRowTypes.QUIZ_RESULT, TestValues.NAME_IDENTIFIER))
                .Returns(this.QuizResultsRepository.Object);

            this.RepositoryFactory.Setup(x => x.CreateRepository<UserConfigurationRepository, IUserConfigurationRepository>(
                It.IsAny<ITableStorageContext>(), CardRowTypes.CONFIGURATION, TestValues.NAME_IDENTIFIER))
                .Returns(this.UserConfigurationRepository.Object);

            this.TableStorageContext = new TableStorageContext(
                this.CloudStorageServices.Object,
                this.RepositoryFactory.Object,
                AzureTableNames.CARD,
                TestValues.NAME_IDENTIFIER);
        }

        protected Mock<ICardRepository> CardRepository { get; set; }
        protected Mock<ICardDeckRepository> CardDeckRepository { get; set; }
        protected Mock<IQuizResultsRepository> QuizResultsRepository { get; set; }
        protected Mock<IUserConfigurationRepository> UserConfigurationRepository { get; set; }
        protected TableStorageContext TableStorageContext { get; private set; }
        protected Mock<IRepositoryFactory> RepositoryFactory { get; private set; }
        protected Mock<ICloudStorageServices> CloudStorageServices { get; private set; }
    }
}