using System.Net;
using System.Net.Http;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Delete_is_called : Given_a_new_CardsController
    {
        private HttpResponseMessage response;

        public override void When()
        {
            this.response = this.CardsController.Delete(TestValues.USER_ID, TestValues.CARD_ID);
        }

        [Test]
        public void Then_Delete_is_called_on_the_CardRepository()
        {
            this.TableStorageContext.Verify(x => x.Cards.DeleteCard(TestValues.USER_ID, TestValues.CARD_ID), Times.Once());
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
    }
}