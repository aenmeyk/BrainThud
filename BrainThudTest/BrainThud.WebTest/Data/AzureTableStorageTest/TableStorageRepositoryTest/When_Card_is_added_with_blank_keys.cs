﻿using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Card_is_added_with_blank_keys : Given_a_new_TableStorageRepository_of_Card
    {
        private readonly Card card = new Card();

        public override void When()
        {
            var tableStorageKeyGenerator = new Mock<ITableStorageKeyGenerator>();
            tableStorageKeyGenerator.Setup(x => x.GeneratePartitionKey()).Returns(TestValues.PARTITION_KEY);
            tableStorageKeyGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.ROW_KEY);

            this.TableStorageRepository.Add(this.card, tableStorageKeyGenerator.Object);
        }

        [Test]
        public void Then_AddObject_is_called_on_the_TableServiceContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.card));
        }

        [Test]
        public void Then_the_PartitionKey_is_set_from_the_TableStorageKeyGenerator()
        {
            this.card.PartitionKey.Should().Be(TestValues.PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_is_set_from_the_TableStorageKeyGenerator()
        {
            this.card.RowKey.Should().Be(TestValues.ROW_KEY);
        }
    }
}