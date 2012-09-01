using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_TableStorageRepository_of_Nugget
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_ITableStorageRepository_should_be_implemented()
        {
            this.TableStorageRepository.Should().BeAssignableTo<ITableStorageRepository<Nugget>>();
        }
    }
}