using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_an_Id_parameter : Given_a_new_CardsController
    {
        private readonly Card expectedResult = new Card();
        private Card actualResult;
        private string rowKey;

        public override void When()
        {
            this.rowKey = CardRowTypes.CARD + "-" + TestValues.CARD_ID;
            this.TableStorageContext.Setup(x => x.Cards.Get(TestValues.NAME_IDENTIFIER, this.rowKey)).Returns(this.expectedResult);
            this.actualResult = this.CardsController.Get(TestValues.CARD_ID);
        }

        [Test]
        public void Then_a_Card_is_returned_from_the_cards_repository()
        {
            this.actualResult.Should().Be(this.expectedResult);
            this.TableStorageContext.Verify(x => x.Cards.Get(TestValues.NAME_IDENTIFIER, this.rowKey), Times.Once());
        }
    }
}