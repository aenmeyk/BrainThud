using System.Web.Http;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.ApiControllerBaseTest
{
    [TestFixture]
    public class When_NothingChanges : Given_a_new_ApiControllerBase
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_the_controller_should_derive_from_ApiController()
        {
            this.ApiControllerBase.Should().BeAssignableTo<ApiController>();
        }
    }
}