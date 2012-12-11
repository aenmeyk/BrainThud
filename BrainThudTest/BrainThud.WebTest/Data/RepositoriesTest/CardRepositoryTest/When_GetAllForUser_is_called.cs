using System.Linq;
using BrainThud.Web.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_GetAllForUser_is_called : Given_a_new_CardRepository
    {
        private IQueryable<Card> actualCards;

        public override void When()
        {
            var cards = Builder<Card>.CreateListOfSize(5)
                .TheFirst(3).With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                .Build();

            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(cards.AsQueryable());
            this.actualCards = this.CardRepository.GetAllForUser();
        }

        [Test]
        public void Then_all_Cards_for_the_user_are_returned_from_the_cards_repository()
        {
            this.actualCards.Should().HaveCount(3);
            this.actualCards.Should().OnlyContain(x => x.PartitionKey == TestValues.CARD_PARTITION_KEY);
        }
    }
}