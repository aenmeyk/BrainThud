using System;
using System.Linq.Expressions;
using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardDeckRepositoryTest
{
    [TestFixture]
    public class When_AddCardToCardDeck_is_called_for_card_deck_that_does_not_exist : Given_a_new_CardDeckRepository
    {
        private const string DECK_NAME = "Nonexistent Deck Name";
        private const string DECK_NAME_SLUG = "nonexistent-deck-name";
        private readonly Card card = new Card { EntityId = TestValues.CARD_ID, UserId = TestValues.USER_ID, DeckName = DECK_NAME };

        public override void When()
        {
            this.CardDeckRepository.AddCardToCardDeck(this.card);
        }

        [Test]
        public void Then_a_new_CardDeck_is_added()
        {
            this.VerifyPropertySet(x => x.DeckName == card.DeckName);
        }

        [Test]
        public void Then_the_DeckNameSlug_is_set()
        {
            this.VerifyPropertySet(x => x.DeckNameSlug == DECK_NAME_SLUG);
        }

        [Test]
        public void Then_the_PartitionKey_is_set()
        {
            this.VerifyPropertySet(x => x.PartitionKey == TestValues.PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_is_set()
        {
            this.VerifyPropertySet(x => x.RowKey == TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_UserId_is_set()
        {
            this.VerifyPropertySet(x => x.UserId == TestValues.USER_ID);
        }

        [Test]
        public void Then_the_EntityId_is_added_to_the_lsit_of_CardIds()
        {
            this.VerifyPropertySet(x => x.CardIds.Contains(this.card.EntityId.ToString()));
        }

        private void VerifyPropertySet(Expression<Func<CardDeck, bool>> propertyCheck)
        {
            this.TableStorageContext.Verify(x => x.AddObject(It.Is(propertyCheck)), Times.Once());
        }
    }
}