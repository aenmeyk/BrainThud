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
        private IQueryable<Card> expectedCards;
        private IQueryable<Card> returnedCards;

        public override void When()
        {
            this.expectedCards = new HashSet<Card> {new Card(), new Card()}.AsQueryable();
            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(this.expectedCards);
            this.returnedCards = this.TableStorageRepository.GetAll();
        }

        [Test]
        public void Then_the_returned_results_are_returned_from_the_TableStorageContext()
        {
            this.returnedCards.Should().Equal(this.expectedCards);
        }
    }
}