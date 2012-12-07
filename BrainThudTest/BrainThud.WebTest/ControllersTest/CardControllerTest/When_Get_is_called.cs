using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_CardsController
    {
        private IQueryable<Card> expectedCards;
        private IEnumerable<Card> returnedCards;

        public override void When()
        {
            this.expectedCards = new HashSet<Card> { new Card(), new Card() }.AsQueryable();
            this.TableStorageContext.Setup(x => x.Cards.GetAll()).Returns(this.expectedCards);
            this.returnedCards = this.CardsController.Get();
        }

        [Test]
        public void Then_all_Cards_are_returned_from_the_cards_repository()
        {
            this.returnedCards.Should().Equal(this.expectedCards);
        }
    }
}