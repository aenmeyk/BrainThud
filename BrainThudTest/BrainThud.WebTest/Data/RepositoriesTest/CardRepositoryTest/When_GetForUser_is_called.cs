using System.Linq;
using BrainThud.Core.Models;
using BrainThud.Web;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_GetForUser_is_called : Given_a_new_CardRepository
    {
        private IQueryable<Card> actualCards;

        public override void When()
        {
            var generator = new UniqueRandomGenerator();
            var cards = Builder<Card>.CreateListOfSize(10)
                .TheFirst(6)
                    .With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                    .And(x => x.RowKey = CardRowTypes.CARD + "-" + generator.Next(1, 100))
                .TheLast(4)
                    .With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                    .And(x => x.RowKey = CardRowTypes.QUIZ_RESULT + "-" + generator.Next(1, 100))
                .Build();

            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(cards.AsQueryable());
            this.actualCards = this.CardRepository.GetForUser();
        }

        [Test]
        public void Then_all_Cards_for_the_user_are_returned_from_the_cards_repository()
        {
            this.actualCards.Should().HaveCount(6);
            this.actualCards.Should().OnlyContain(x => x.PartitionKey == TestValues.CARD_PARTITION_KEY && x.RowKey.StartsWith(CardRowTypes.CARD + "-"));
        }
    }
}