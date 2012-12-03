using System;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using BrainThud.Web;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;
using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    [Category(TestTypes.LONG_RUNNING)]
    public class When_Post_is_called : Given_a_new_CardController
    {
        private Card card;
        private HttpResponseMessage response;

        public override void When()
        {
            this.card = new Card { RowKey = "1C81541E-5062-43F5-B63D-E07BD579FE79" };
            this.response = this.CardsController.Post(this.card);
        }

        [Test]
        public void Then_Add_is_called_on_Card_repository()
        {
            this.TableStorageContext.Verify(x => x.Cards.Add(this.card, It.IsAny<ITableStorageKeyGenerator>()), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_an_HttpResponseMessage_is_returned_with_a_201_status_code()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void Then_the_Card_should_be_returned_in_the_response()
        {
            var task = this.response.Content.ReadAsStringAsync();
            var cardInResponse = Json.Decode(task.Result);
            ((string)cardInResponse.RowKey).Should().Be(this.card.RowKey);
        }

        [Test]
        public void Then_the_location_should_be_created_from_the_cards_RowKey()
        {
            var type = this.CardsController.RouteValues.GetType();
            var propertyInfo = type.GetProperty("id");
            var id = propertyInfo.GetValue(this.CardsController.RouteValues, null);

            id.Should().Be(this.card.RowKey);
            this.CardsController.RouteName.Should().Be(RouteNames.API_DEFAULT);
            this.response.Headers.Location.ToString().Should().Be(TestUrls.LOCALHOST);
        }
    }
}