using System;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_Add_is_called : Given_a_new_CardRepository
    {
        private readonly Card card = new Card();

        public override void When()
        {
            var keyGenerator = new Mock<ICardEntityKeyGenerator>();
            keyGenerator.Setup(x => x.GeneratePartitionKey()).Returns(TestValues.PARTITION_KEY);
            keyGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.ROW_KEY);
            keyGenerator.SetupGet(x => x.GeneratedUserId).Returns(TestValues.USER_ID);
            keyGenerator.SetupGet(x => x.GeneratedEntityId).Returns(TestValues.CARD_ID);

            this.CardRepository.Add(card, keyGenerator.Object);
        }

        [Test]
        public void Then_the_PartitionKey_should_be_set()
        {
            this.card.PartitionKey.Should().Be(TestValues.PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_should_be_set()
        {
            this.card.RowKey.Should().Be(TestValues.ROW_KEY);
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
        public void Then_the_QuizDate_should_be_set_for_tomorrow()
        {
            this.card.QuizDate.Date.Should().Be(DateTime.UtcNow.AddDays(1).Date);
        }
    }
}