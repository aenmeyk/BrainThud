using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Model;
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
                .TheFirst(3)
                    .With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                .TheFirst(1)
                    .And(x => x.RowKey = TestValues.CARD_ROW_KEY)
                .Build();

            this.TableStorageContext = new Mock<ITableStorageContext>();
            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(cards.AsQueryable());

            this.CardRepository = new CardRepository(this.TableStorageContext.Object, TestValues.NAME_IDENTIFIER);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected CardRepository CardRepository { get; private set; }
    }
}