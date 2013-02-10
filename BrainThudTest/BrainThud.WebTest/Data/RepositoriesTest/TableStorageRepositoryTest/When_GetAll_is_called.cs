using System.Collections.Generic;
using System.Linq;
using BrainThud.Core.Models;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_GetAll_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private IQueryable<Card> actualCards;
        private IList<Card> expectedCards;

        public override void When()
        {
            this.expectedCards = Builder<Card>.CreateListOfSize(5).Build();
            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(this.expectedCards.AsQueryable());
            this.actualCards = this.TableStorageRepository.GetAll();
        }

        [Test]
        public void Then_all_Cards_are_returned_from_the_cards_repository()
        {
            this.actualCards.Should().BeEquivalentTo(this.expectedCards);
        }
    }
}