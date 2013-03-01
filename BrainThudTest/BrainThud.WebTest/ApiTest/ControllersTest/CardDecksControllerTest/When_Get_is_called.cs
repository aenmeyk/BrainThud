using System.Collections.Generic;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardDecksControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_CardDecksController
    {
        private IEnumerable<CardDeck> results;
        private IList<CardDeck> cardDecks;

        public override void When()
        {
            this.cardDecks = Builder<CardDeck>.CreateListOfSize(10)
                .TheFirst(1).With(x => x.DeckName = "z")
                .TheNext(1).With(x => x.DeckName = "a")
                .TheLast(1).With(x => x.DeckName = "m")
                .Build();
            this.TableStorageContext.Setup(x => x.CardDecks.GetForUser()).Returns(this.cardDecks.AsQueryable());

            this.results = this.CardDecksController.Get();
        }

        [Test]
        public void Then_the_card_decks_are_returned_from_the_TableStorageContext()
        {
            this.results.ShouldBeEquivalentTo(cardDecks);
        }

        [Test]
        public void Then_the_CardDecks_should_be_in_alphabetical_order()
        {
            this.results.Select(x => x.DeckName).Should().BeInAscendingOrder();
        }
    }
}