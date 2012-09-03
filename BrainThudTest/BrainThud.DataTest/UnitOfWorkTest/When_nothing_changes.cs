using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using BrainThudTest.Extensions;
using BrainThud.Model;

namespace BrainThudTest.BrainThud.DataTest.UnitOfWorkTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_UnitOfWork
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_IUnitOfWork_should_be_implemented()
        {
            this.UnitOfWork.Should().BeAssignableTo<IUnitOfWork>();
        }

        [Test]
        public void Then_Nuggets_property_should_get_and_set_an_ITableStorageRepository()
        {
            this.UnitOfWork.CanGetSetValue(x => x.Nuggets, new Mock<ITableStorageRepository<Nugget>>().Object, typeof(ITableStorageRepository<Nugget>));
        }
    }
}