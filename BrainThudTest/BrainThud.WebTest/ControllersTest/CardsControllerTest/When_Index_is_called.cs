using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Index_is_called : Given_a_new_CardsController
    {
        private ViewResult result;
        private IList<Card> cards;

        public override void When()
        {
            this.cards = Builder<Card>.CreateListOfSize(10)
                .All()
                    .With(x => x.UserId = TestValues.USER_ID)
                .Random(5)
                    .With(x => x.DeckName = TestValues.STRING)
                    .And(x => x.DeckNameSlug = TestValues.STRING)
                .Build();

            this.TableStorageContext.Setup(x => x.Cards.GetAll()).Returns(this.cards.AsQueryable);
            this.result = (ViewResult)this.LibraryController.Index();
        }

        [Test]
        public void Then_a_View_is_returned()
        {
            this.result.ViewName.Should().Be(string.Empty);
        }

        [Test]
        public void Then_the_view_model_should_be_a_list_of_CardDecks()
        {
            this.result.Model.Should().BeAssignableTo<IEnumerable<CardDeck>>();
        }

        [Test]
        public void Then_the_cardDeckNames_should_be_returned_from_the_TableStorageContext()
        {
            ((IEnumerable<CardDeck>)this.result.Model)
                .Select(x => x.DeckName)
                .Should()
                .BeEquivalentTo(this.cards.Select(x => x.DeckName));
        }

        [Test]
        public void Then_the_card_deck_names_should_be_unique()
        {
            ((IEnumerable<CardDeck>)this.result.Model)
                .Select(x => x.DeckName)
                .ToList()
                .Should()
                .OnlyHaveUniqueItems();
        }

        [Test]
        public void Then_the_card_deck_names_should_be_in_ascending_order()
        {
            ((IEnumerable<CardDeck>)this.result.Model)
                .Select(x => x.DeckName)
                .ToList()
                .Should()
                .BeInAscendingOrder();
        }
    }
}