using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_GetAll_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private IQueryable<Card> returnedCards;
        private Card card1;
        private Card card2;
        private Card card3;

        public override void When()
        {
            this.card1 = new Card { PartitionKey = TestValues.PARTITION_KEY };
            this.card2 = new Card { PartitionKey = "DifferentPartitionKey" };
            this.card3 = new Card { PartitionKey = TestValues.PARTITION_KEY };

            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(new HashSet<Card> { this.card1, this.card2, this.card3 }.AsQueryable());
            this.returnedCards = this.TableStorageRepository.GetAll();
        }

        [Test]
        public void Then_all_Cards_for_the_authenticated_user_are_returned_from_the_cards_repository()
        {
            this.returnedCards.Should().Contain(new[] { card1, this.card3 }).And.NotContain(this.card2);
        }
    }
}