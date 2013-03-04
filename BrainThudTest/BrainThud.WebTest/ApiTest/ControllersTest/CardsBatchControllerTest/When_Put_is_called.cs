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
        private readonly Card originalCard = new Card();
        private HttpResponseMessage response;
        private IList<Card> cards;

        public override void When()
        {
            this.cards = Builder<Card>.CreateListOfSize(10).Build();

            this.TableStorageContext
                .Setup(x => x.Cards.Get(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(this.originalCard);

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
        public void Then_Update_should_be_called_on_the_card_repository_for_each_card()
        {
            foreach (var card in cards)
            {
                var item = card;
                this.TableStorageContext.Verify(x => x.Cards.Update(item), Times.Once());
            }
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_the_original_card_deck_name_is_removed_from_each_card()
        {
            this.TableStorageContext.Verify(x => 
                x.CardDecks.RemoveCardFromCardDeck(this.originalCard), Times.Exactly(this.cards.Count));
        }

        [Test]
        public void Then_the_original_card_should_be_detached()
        {
            this.TableStorageContext.Verify(x => x.Detach(this.originalCard), Times.Exactly(this.cards.Count));
        }

        [Test]
        public void Then_the_new_card_deck_name_is_added()
        {
            foreach (var card in cards)
            {
                var item = card;
                this.TableStorageContext.Verify(x => x.CardDecks.AddCardToCardDeck(item));
            }
        }
    }
}