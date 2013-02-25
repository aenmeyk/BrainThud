using System.Linq;
using BrainThud.Core.Models;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_CardDeckRepository : Gwt
    {
        public override void Given()
        {
            var cardDecks = Builder<CardDeck>.CreateListOfSize(5).Build();

            this.CardDeck = new CardDeck
            {
                DeckName = TestValues.DECK_NAME,
                PartitionKey = TestValues.CARD_PARTITION_KEY,
                RowKey = TestValues.CARD_DECK_ROW_KEY
            };

            cardDecks.Add(this.CardDeck);

            var cardKeyGenerator = new Mock<ICardEntityKeyGenerator>();
            cardKeyGenerator.Setup(x => x.GeneratePartitionKey(TestValues.USER_ID)).Returns(TestValues.PARTITION_KEY);
            cardKeyGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.ROW_KEY);

            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            this.TableStorageContext.Setup(x => x.CreateQuery<CardDeck>()).Returns(cardDecks.AsQueryable());
            this.TableStorageContext.Setup(x => x.UserConfigurations.GetByNameIdentifier()).Returns(new UserConfiguration { UserId = TestValues.USER_ID });

            this.CardDeckRepository = new CardDeckRepository(
                this.TableStorageContext.Object,
                cardKeyGenerator.Object,
                TestValues.NAME_IDENTIFIER);
        }

        protected CardDeck CardDeck { get; set; }
        protected CardDeckRepository CardDeckRepository { get; set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
    }
}