using System;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_Add_is_called : Given_a_new_CardRepository
    {
        private readonly Card card = new Card { DeckName = "the deck name" };

        public override void When()
        {
            this.CardRepository.Add(this.card, TestValues.CARD_QUIZ_DATE);
        }

        [Test]
        public void Then_the_PartitionKey_should_be_set()
        {
            this.card.PartitionKey.Should().Be(TestValues.CARD_PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_should_be_set()
        {
            this.card.RowKey.Should().Be(TestValues.CARD_ROW_KEY);
        }

        [Test]
        public void Then_the_UserId_should_be_set()
        {
            this.card.UserId.Should().Be(TestValues.USER_ID);
        }

        [Test]
        public void Then_the_EntityId_should_be_set()
        {
            this.card.EntityId.Should().Be(TestValues.CARD_ID);
        }

        [Test]
        public void Then_the_QuizDate_should_be_set()
        {
            this.card.QuizDate.Should().BeWithin(10.Seconds()).Before(TestValues.CARD_QUIZ_DATE);
        }

        [Test]
        public void Then_the_DeckNameSlug_should_be_populated()
        {
            this.card.DeckNameSlug.Should().Be("the-deck-name");
        }
    }
}