using System.Net;
using System.Net.Http;
using BrainThud.Core.Models;
using BrainThud.Core.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Put_is_called : Given_a_new_CardsController
    {
        private Card card;
        private HttpResponseMessage response;

        public override void When()
        {
            this.card = new Card();
            this.TableStorageContext
                .Setup(x => x.Cards.Update(this.card))
                .Callback<Card>(x => x.EntityId = TestValues.CARD_ID);
  
            this.response = this.CardsController.Put(this.card);
        }

        [Test]
        public void Then_an_HttpResponseMessage_is_returned()
        {
            this.response.Should().BeAssignableTo<HttpResponseMessage>();
        }

        [Test]
        public void Then_the_returned_status_code_should_be_200()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Then_the_updated_card_should_be_returned_in_the_response()
        {
            this.response.Content.As<ObjectContent>().Value.As<Card>().EntityId.Should().Be(TestValues.CARD_ID);
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