using System.Collections.Generic;
using System.Linq;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Get_is_called_with_a_partition_key : Given_a_new_TableStorageRepository_of_Card
    {
        private IList<Card> cards;
        private IQueryable<Card> result;

        public override void When()
        {
            this.cards = Builder<Card>.CreateListOfSize(10)
                .Random(5).With(x => x.PartitionKey = TestValues.PARTITION_KEY)
                .Build();

            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(this.cards.AsQueryable());
            this.result = this.TableStorageRepository.Get(TestValues.PARTITION_KEY);
        }

        [Test]
        public void Then_all_Cards_are_returned_from_the_cards_repository()
        {
            this.result.Should().OnlyContain(x => x.PartitionKey == TestValues.PARTITION_KEY);
        }
    }
}