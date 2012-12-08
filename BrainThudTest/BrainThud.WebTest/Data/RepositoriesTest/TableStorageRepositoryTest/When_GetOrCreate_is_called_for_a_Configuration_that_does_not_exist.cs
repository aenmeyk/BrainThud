using System.Collections.ObjectModel;
using System.Linq;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_GetOrCreate_is_called_for_a_Configuration_that_does_not_exist : Given_a_new_TableStorageRepository_of_Configuration
    {
        private UserConfiguration result;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.CreateQuery<UserConfiguration>()).Returns(new Collection<UserConfiguration>().AsQueryable());
            this.result = this.TableStorageRepository.GetOrCreate(TestValues.PARTITION_KEY, TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_Configuration_entity_should_be_created()
        {
            this.result.PartitionKey.Should().Be(TestValues.PARTITION_KEY);
            this.result.RowKey.Should().Be(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_new_Configuration_should_be_added_to_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.result), Times.Once());
        }
    }
}