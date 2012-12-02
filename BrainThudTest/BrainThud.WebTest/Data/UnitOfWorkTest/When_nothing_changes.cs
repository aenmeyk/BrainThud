using BrainThud.Web.Data;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using BrainThudTest.Extensions;

namespace BrainThudTest.BrainThud.WebTest.Data.UnitOfWorkTest
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
        public void Then_Cards_property_should_get_and_set_an_ITableStorageRepository()
        {
            this.UnitOfWork.CanGetSetValue(x => x.Cards, new Mock<ITableStorageRepository<Card>>().Object, typeof(ITableStorageRepository<Card>));
        }

        [Test]
        public void Then_QuizResults_property_should_get_and_set_an_ITableStorageRepository()
        {
            this.UnitOfWork.CanGetSetValue(x => x.QuizResults, new Mock<ITableStorageRepository<QuizResult>>().Object, typeof(ITableStorageRepository<QuizResult>));
        }
    }
}