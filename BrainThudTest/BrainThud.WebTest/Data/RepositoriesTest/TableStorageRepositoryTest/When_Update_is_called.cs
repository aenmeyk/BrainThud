using System;
using BrainThud.Web.Model;
using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Update_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private Card card;
        private readonly DateTime createdTimestamp = TestValues.DATETIME;

        public override void When()
        {
            this.card = new Card { CreatedTimestamp = this.createdTimestamp };
            this.TableStorageRepository.Update(this.card);
        }

        [Test]
        public void Then_UpdateObject_is_called_on_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.UpdateObject(this.card), Times.Once());
        }

        [Test]
        public void Then_the_CreatedTimestamp_should_remain_the_same()
        {
            this.card.CreatedTimestamp.Should().Be(this.createdTimestamp);
        }
    }
}