using BrainThud.Core.AzureServices;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.CoreTest.AzureServicesTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_AccessControlService
    {
        [Test]
        public void Then_IAccessControlService_should_be_implemented()
        {
            this.AccessControlService.Should().BeAssignableTo<IAccessControlService>();
        }
    }
}