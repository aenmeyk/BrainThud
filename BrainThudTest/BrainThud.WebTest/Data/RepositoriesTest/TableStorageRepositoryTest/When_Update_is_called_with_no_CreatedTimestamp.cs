using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Update_is_called_with_no_CreatedTimestamp : Given_a_new_TableStorageRepository_of_Card
    {
        private Card card;

        public override void When()
        {
            this.card = new Card { Timestamp = TestValues.DATETIME };
            this.TableStorageRepository.Update(this.card);
        }

        [Test]
        public void Then_the_CreatedTimestamp_should_set_from_the_Timestamp()
        {
            this.card.CreatedTimestamp.Should().Be(this.card.Timestamp);
        }
    }
}