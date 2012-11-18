using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;
using BrainThud.Model;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_an_Id_parameter : Given_a_new_CardController
    {
        private readonly Card expectedResult = new Card();
        private Card actualResult;

        public override void When()
        {
            this.UnitOfWork.Setup(x => x.Cards.Get(TestValues.ROW_KEY)).Returns(this.expectedResult);
            this.actualResult = this.CardsController.Get(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_a_Card_is_returned_from_the_cards_repository()
        {
            this.actualResult.Should().Be(this.expectedResult);
            this.UnitOfWork.Verify(x => x.Cards.Get(TestValues.ROW_KEY));
        }
    }
}