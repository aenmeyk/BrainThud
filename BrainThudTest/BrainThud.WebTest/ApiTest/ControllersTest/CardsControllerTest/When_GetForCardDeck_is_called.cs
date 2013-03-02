using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_GetForCardDeck_is_called : Given_a_new_CardsController
    {
        private IEnumerable<Card> cards;

        public override void When()
        {
            var userCards = Builder<Card>.CreateListOfSize(10)
                .Random(5).With(x => x.DeckNameSlug = TestValues.DECK_NAME_SLUG)
                .Build();

            this.TableStorageContext.Setup(x => x.Cards.GetForUser()).Returns(userCards.AsQueryable());
            this.cards = this.CardsController.GetForCardDeck(TestValues.DECK_NAME_SLUG);
        }

        [Test]
        public void Then_only_cards_with_a_DeckNameSlug_equal_to_the_deckNameSlug_parameter_are_returned()
        {
            this.cards.Should().OnlyContain(x => x.DeckNameSlug == TestValues.DECK_NAME_SLUG).And.HaveCount(5);
        }
    }
}