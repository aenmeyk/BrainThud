using BrainThud.Data;
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
        public void Then_Nuggets_property_should_get_and_set_an_IRepository()
        {
            this.UnitOfWork.CanGetSetValue(x => x.Nuggets, new Mock<IRepository<Nugget>>().Object, typeof(IRepository<Nugget>));
        }
    }
}