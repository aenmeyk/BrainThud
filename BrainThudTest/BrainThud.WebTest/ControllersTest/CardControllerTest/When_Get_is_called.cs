using System.Collections.Generic;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_CardController
    {
        private readonly IEnumerable<Card> expectedCards = new HashSet<Card> { new Card(), new Card() };
        private IEnumerable<Card> returnedCards;

        public override void When()
        {
            this.UnitOfWork.Setup(x => x.Cards.GetAll()).Returns(this.expectedCards);
            this.returnedCards = this.CardsController.Get();
        }

        [Test]
        public void Then_all_Cards_are_returned_from_UnitOfWork()
        {
            this.returnedCards.Should().Equal(this.expectedCards);
        }
    }
}