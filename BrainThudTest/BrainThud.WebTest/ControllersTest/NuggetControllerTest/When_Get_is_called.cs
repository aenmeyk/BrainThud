using System.Collections.Generic;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_NuggetController
    {
        private readonly IEnumerable<Nugget> expectedNuggets = new HashSet<Nugget> { new Nugget(), new Nugget() };
        private IEnumerable<Nugget> returnedNuggets;

        public override void When()
        {
            this.UnitOfWork.Setup(x => x.Nuggets.GetAll()).Returns(this.expectedNuggets);
            this.returnedNuggets = this.NuggetController.Get();
        }

        [Test]
        public void Then_all_Nuggets_are_returned_from_UnitOfWork()
        {
            this.returnedNuggets.Should().Equal(this.expectedNuggets);
        }
    }
}