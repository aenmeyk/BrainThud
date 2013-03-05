using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Delete_is_called : Given_a_new_CardsController
    {
        private HttpResponseMessage response;
        private IList<Card> cards;

        public override void When()
        {
            this.cards = Builder<Card>.CreateListOfSize(10).Build();
            this.response = this.CardsController.Delete(this.cards);
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
        public void Then_all_cards_should_be_deleted_using_the_TableStorageContext()
        {
            foreach(var card in cards)
            {
                var cardItem = card;
                this.TableStorageContext.Verify(x => x.DeleteCardAndRelations(cardItem), Times.Once());
            }
        }
    }
}