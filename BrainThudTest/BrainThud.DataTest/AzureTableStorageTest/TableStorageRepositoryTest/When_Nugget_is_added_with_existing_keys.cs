using System;
using BrainThud.Model;
using NUnit.Framework;
using FluentAssertions;
using BrainThud.Data;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Nugget_is_added_with_existing_keys : Given_a_new_TableStorageRepository_of_Nugget
    {
        private const string PARTITION_KEY = "ed518ec1-5133-4ed7-8ced-5636e1c12d2a";
        private const string ROW_KEY = "746fecff-fcbf-4da0-b8c1-cd08fd06b1e8";
        private readonly Nugget nugget = new Nugget { PartitionKey = PARTITION_KEY, RowKey = ROW_KEY };

        public override void When()
        {
            this.TableStorageRepository.Add(this.nugget);
        }

        [Test]
        public void Then_the_PartitionKey_should_not_change()
        {
            this.nugget.PartitionKey.Should().Be(PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_should_not_change()
        {
            this.nugget.RowKey.Should().Be(ROW_KEY);
        }
    }
}