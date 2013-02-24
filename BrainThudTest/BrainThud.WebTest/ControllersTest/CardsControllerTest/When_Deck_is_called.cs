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
    public class When_Deck_is_called : Given_a_new_CardsController
    {
        private ViewResult result;
        private IList<Card> cards;

        public override void When()
        {
            this.cards = Builder<Card>.CreateListOfSize(10)
              .All()
                .With(x => x.PartitionKey = TestValues.PARTITION_KEY)
                .And(x => x.UserId = TestValues.USER_ID)
              .Random(5)
                .With(x => x.DeckNameSlug = TestValues.STRING.ToLower())
              .Build();

            this.TableStorageContext.Setup(x => x.Cards.Get(TestValues.PARTITION_KEY)).Returns<string>(x => this.cards.AsQueryable());
            this.result = (ViewResult)this.LibraryController.Deck(TestValues.USER_ID, TestValues.STRING.ToUpper());
        }

        [Test]
        public void Then_the_card_deck_View_is_returned()
        {
            this.result.ViewName.Should().Be("card-deck");
        }

        [Test]
        public void Then_the_view_model_should_be_a_list_of_Cards()
        {
            this.result.Model.Should().BeAssignableTo<IEnumerable<Card>>();
        }

        [Test]
        public void Then_the_cards_should_be_returned_from_the_TableStorageContext()
        {
            ((IEnumerable<Card>)this.result.Model)
                .Should()
                .OnlyContain(x => x.UserId == TestValues.USER_ID && x.DeckNameSlug == TestValues.STRING.ToLower());
        }
    }
}