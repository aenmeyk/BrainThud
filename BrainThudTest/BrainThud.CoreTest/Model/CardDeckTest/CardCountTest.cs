using BrainThud.Core.Models;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.CoreTest.Model.CardDeckTest
{
    [TestFixture]
    public class CardCountTest
    {
        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(" ", 0)]
        [TestCase("1,2,3,4,5", 5)]
        public void CardCount_should_match_number_of_CardIds(string cardIds, int expectedCardCount)
        {
            var cardDeck = new CardDeck { CardIds = cardIds };
            cardDeck.CardCount.Should().Be(expectedCardCount);
        }
    }
}