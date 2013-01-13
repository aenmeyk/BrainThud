using System;
using System.Collections.Generic;
using System.Linq;
using BrainThud.Web.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_CardsController
    {
        private IQueryable<Card> expectedCards;
        private IEnumerable<Card> returnedCards;

        public override void When()
        {
            var generator = new UniqueRandomGenerator();

            this.expectedCards = Builder<Card>
                .CreateListOfSize(10)
                .All().With(x => x.CreatedTimestamp = generator.Next(DateTime.MinValue, DateTime.MaxValue))
                .Build()
                .AsQueryable();

            this.TableStorageContext.Setup(x => x.Cards.GetForUser()).Returns(this.expectedCards);
            this.returnedCards = this.CardsController.Get();
        }

        [Test]
        public void Then_all_Cards_are_returned_from_the_cards_repository()
        {
            this.returnedCards.Should().BeEquivalentTo(this.expectedCards);
        }

        [Test]
        public void Then_the_cards_should_be_returned_in_CreatedTimestamp_order()
        {
            this.returnedCards.Select(x => x.CreatedTimestamp).Should().BeInAscendingOrder();
        }
    }
}