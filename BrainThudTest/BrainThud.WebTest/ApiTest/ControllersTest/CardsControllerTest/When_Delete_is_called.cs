using System.Net;
using System.Net.Http;
using BrainThud.Core.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Delete_is_called : Given_a_new_CardsController
    {
        private HttpResponseMessage response;
        private Card card;

        public override void When()
        {
            this.card = new Card { UserId = TestValues.USER_ID, EntityId = TestValues.CARD_ID };
//            this.response = this.CardsController.Delete(this.card);
        }

        [Test]
        public void Then_Delete_is_called_on_the_CardRepository()
        {
            this.TableStorageContext.Verify(x => x.Cards.DeleteById(TestValues.USER_ID, TestValues.CARD_ID), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_the_cards_repository()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_the_return_status_code_should_be_204()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public void Then_the_associated_QuizResults_are_deleted()
        {
            this.TableStorageContext.Verify(x => x.QuizResults.DeleteByCardId(TestValues.CARD_ID), Times.Once());
        }

        [Test]
        public void Then_RemoveCardFromCardDeck_is_called()
        {
            this.TableStorageContext.Verify(x => x.CardDecks.RemoveCardFromCardDeck(this.card));
        }
    }
}