using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsBatchControllerTest
{
    [TestFixture]
    public class When_Put_is_called : Given_a_new_CardsBatchController
    {
        private HttpResponseMessage response;
        private IList<Card> cards;

        public override void When()
        {
            this.cards = Builder<Card>.CreateListOfSize(10).Build();
            this.response = this.CardsBatchController.Put(this.cards);
        }

        [Test]
        public void Then_the_returned_status_code_should_be_200()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Then_the_updated_cards_should_be_returned_in_the_response()
        {
            this.response
                .Content.As<ObjectContent>()
                .Value.As<IEnumerable<Card>>()
                .Should().BeEquivalentTo(this.cards);
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_all_Cards_and_related_items_should_be_updated()
        {
            foreach(var card in cards)
            {
                var cardItem = card;
                this.TableStorageContext.Verify(x => x.UpdateCardAndRelations(cardItem), Times.Once());
            }
        }
    }
}