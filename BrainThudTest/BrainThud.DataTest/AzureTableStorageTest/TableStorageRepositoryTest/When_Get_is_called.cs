using System.Collections.Generic;
using BrainThud.Model;
using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;
using BrainThud.Data;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_TableStorageRepository_of_Nugget
    {
        private Nugget actualResult;
        private Nugget expectedResult;

        public override void When()
        {
            this.expectedResult = new Nugget { PartitionKey = Keys.TEMP_PARTITION_KEY, RowKey = TestValues.ROW_KEY };
            var nuggets = new HashSet<Nugget> { this.expectedResult, new Nugget() };
            this.TableStorageContext.Setup(x => x.CreateQuery(typeof(Nugget).Name)).Returns(nuggets);
            this.actualResult = this.TableStorageRepository.Get(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_nugget_should_be_returned_from_the_StorageContext()
        {
            this.actualResult.Should().Be(this.expectedResult);
        }
    }
}