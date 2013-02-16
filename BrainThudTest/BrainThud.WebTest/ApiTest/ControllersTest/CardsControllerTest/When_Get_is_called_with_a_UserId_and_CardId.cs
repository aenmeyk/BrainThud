using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_a_UserId_and_CardId : Given_a_new_CardsController
    {
        private readonly Card expectedResult = new Card();
        private Card actualResult;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.GetById(TestValues.USER_ID, TestValues.CARD_ID)).Returns(this.expectedResult);
            this.actualResult = this.CardsController.Get(TestValues.USER_ID, TestValues.CARD_ID);
        }

        [Test]
        public void Then_a_Card_is_returned_from_the_cards_repository()
        {
            this.actualResult.Should().Be(this.expectedResult);
        }
    }
}