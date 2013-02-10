using BrainThud.Core.Models;
using BrainThud.Web.Data.Repositories;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_TableStorageRepository_of_Card
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_ITableStorageRepository_should_be_implemented()
        {
            this.TableStorageRepository.Should().BeAssignableTo<ITableStorageRepository<Card>>();
        }
    }
}