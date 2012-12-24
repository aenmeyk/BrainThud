using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.ConfigControllerTest
{
    [TestFixture]
    public class When_Get_is_called : Given_a_new_ConfigController
    {
        private readonly UserConfiguration expectedResult = new UserConfiguration();
        private UserConfiguration actualResult;

        public override void When()
        {
//            this.TableStorageContextFactory
//                .Setup(x => x.CreateTableStorageContext().GetByNameIdentifier())
//                .Returns(this.expectedResult);

            this.actualResult = this.ConfigController.Get();
        }

        [Test]
        public void Then_the_userId_should_be_returned_in_the_config()
        {
            this.actualResult.Should().Be(this.expectedResult);
        }
    }
}