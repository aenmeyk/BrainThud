using System;
using BrainThud.Core.Models;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Core.Models;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Card_is_added_with_blank_keys : Given_a_new_TableStorageRepository_of_Card
    {
        private readonly Card card = new Card();

        public override void When()
        {
            var tableStorageKeyGenerator = new Mock<ITableStorageKeyGenerator>();
            tableStorageKeyGenerator.Setup(x => x.GeneratePartitionKey(TestValues.USER_ID)).Returns(TestValues.PARTITION_KEY);
            tableStorageKeyGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.ROW_KEY);

            this.TableStorageRepository.Add(this.card);
        }

        [Test]
        public void Then_AddObject_is_called_on_the_TableServiceContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.card));
        }

        [Test]
        public void Then_CreatedTimestamp_is_set_on_the_card()
        {
            this.card.CreatedTimestamp.Should().BeWithin(5.Seconds()).Before(DateTime.UtcNow);
        }
    }
}