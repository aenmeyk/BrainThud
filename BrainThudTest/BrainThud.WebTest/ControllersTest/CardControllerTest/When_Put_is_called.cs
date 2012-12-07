using System.Net;
using System.Net.Http;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Put_is_called : Given_a_new_CardsController
    {
        private Card card;
        private HttpResponseMessage response;

        public override void When()
        {
            this.card = new Card();
            this.response = this.CardsController.Put(this.card);
        }

        [Test]
        public void Then_an_HttpResponseMessage_is_returned()
        {
            this.response.Should().BeAssignableTo<HttpResponseMessage>();
        }

        [Test]
        public void Then_the_returned_status_code_should_be_204()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public void Then_Update_should_be_called_on_the_card_repository()
        {
            this.TableStorageContext.Verify(x => x.Cards.Update(this.card), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }
    }
}