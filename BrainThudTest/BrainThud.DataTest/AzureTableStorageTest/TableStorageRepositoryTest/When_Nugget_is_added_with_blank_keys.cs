using BrainThud.Model;
using NUnit.Framework;
using FluentAssertions;
using BrainThud.Data;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Nugget_is_added_with_blank_keys : Given_a_new_TableStorageRepository_of_Nugget
    {
        private readonly Nugget nugget = new Nugget();

        public override void When()
        {
            this.TableStorageRepository.Add(this.nugget);
        }

        [Test]
        public void Then_AddObject_is_called_on_the_TableServiceContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.nugget));
        }

        [Test]
        public void Then_the_PartitionKey_is_set_from_the_TableStorageKeyGenerator()
        {
            this.nugget.PartitionKey.Should().Be(PARTITION_KEY);
        }

        [Test]
        public void Then_the_RowKey_is_set_from_the_TableStorageKeyGenerator()
        {
            this.nugget.RowKey.Should().Be(ROW_KEY);
        }
    }
}