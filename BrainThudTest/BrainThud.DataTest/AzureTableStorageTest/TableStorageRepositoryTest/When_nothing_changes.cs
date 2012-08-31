using BrainThud.Data;
using FluentAssertions;
using NUnit.Framework;
using BrainThud.Model;

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
        public void Then_IRepository_should_be_implemented()
        {
            this.TableStorageRepository.Should().BeAssignableTo<IRepository<Nugget>>();
        }
    }
}