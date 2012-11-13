using System.Net;
using System.Net.Http;
using BrainThud.Model;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Post_is_called_with_an_invalid_model : Given_a_new_CardController
    {
        private Card card;
        private HttpResponseMessage response;

        public override void When()
        {
            this.card = new Card();
            this.CardsController.ModelState.AddModelError("Error Key", "Error Message");
            this.response = this.CardsController.Post(this.card);
        }

        [Test]
        public void Then_the_returned_status_code_should_be_400()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}