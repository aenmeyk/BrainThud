using BrainThud.Data;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.CardKeyGeneratorTest
{
    [TestFixture]
    public class When_GenerateRowKey_is_called : Given_a_new_CardKeyGenerator
    {
        private string rowKey;

        public override void When()
        {
            this.rowKey = this.CardKeyGenerator.GenerateRowKey();
        }

        [Test]
        public void Then_the_RowKey_should_start_with_the_row_type()
        {
            this.rowKey.Substring(0, 1).Should().Be(CardRowTypes.CARD);
        }

        [Test]
        public void Then_the_UserId_should_be_part_of_the_RowKey()
        {
            this.rowKey.Substring(4, 1).Should().Be(this.user.Id.ToString());
        }

        [Test]
        public void Then_the_unique_card_ID_should_be_part_of_the_RowKey()
        {
            this.rowKey.Substring(2, 1).Should().Be((CARD_ID + 1).ToString());
        }

        [Test]
        public void Then_the_LastUsedCardId_is_incremented()
        {
            this.user.LastUsedCardId.Should().Be(CARD_ID + 1);
        }

        [Test]
        public void Then_the_new_LastUsedCardId_should_be_persisted()
        {
            this.UserRepository.Verify(x => x.Update(this.user));
            this.UserRepository.Verify(x => x.Commit(), Times.Once());
        }
    }
}