using BrainThud.Core.Models;
using BrainThud.Core.Calendars;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_CardRepository : Gwt
    {
        public override void Given()
        {
            var cards = Builder<Card>.CreateListOfSize(5)
                .TheFirst(3).With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                .TheFirst(1).And(x => x.RowKey = TestValues.CARD_ROW_KEY)
                .Build();

            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(cards.AsQueryable());
            var userConfiguration = new UserConfiguration { UserId = TestValues.USER_ID };
            this.TableStorageContext
                .Setup(x => x.UserConfigurations.GetByNameIdentifier())
                .Returns(userConfiguration);

            this.CardKeyGenerator = new Mock<ICardEntityKeyGenerator>();
            this.CardKeyGenerator.Setup(x => x.GeneratePartitionKey(TestValues.USER_ID)).Returns(TestValues.CARD_PARTITION_KEY);
            this.CardKeyGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.CARD_ROW_KEY);
            this.CardKeyGenerator.Setup(x => x.GetPartitionKey(TestValues.USER_ID)).Returns(TestValues.CARD_PARTITION_KEY);
            this.CardKeyGenerator.Setup(x => x.GetRowKey(TestValues.CARD_ID)).Returns(TestValues.CARD_ROW_KEY);
            this.CardKeyGenerator.SetupGet(x => x.GeneratedEntityId).Returns(TestValues.CARD_ID);

            this.QuizCalendar = new Mock<IQuizCalendar>();

            this.CardRepository = new CardRepository(
                this.TableStorageContext.Object,
                this.CardKeyGenerator.Object,
                this.QuizCalendar.Object,
                TestValues.NAME_IDENTIFIER);
        }

        protected Mock<IQuizCalendar> QuizCalendar { get; private set; }
        protected Mock<ICardEntityKeyGenerator> CardKeyGenerator { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected CardRepository CardRepository { get; private set; }
    }
}