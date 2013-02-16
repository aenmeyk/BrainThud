using BrainThud.Web.Api.Controllers;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.ConfigControllerTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_ConfigController
    {
        [Test]
        public void Then_the_controller_should_derive_from_ApiControllerBase()
        {
            this.ConfigController.Should().BeAssignableTo<ApiControllerBase>();
        }
    }
}