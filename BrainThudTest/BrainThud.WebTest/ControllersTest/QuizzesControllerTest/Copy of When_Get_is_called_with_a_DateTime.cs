using BrainThud.Web.Dtos;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizzesControllerTest
{
    [TestFixture]
    public class When_Get_is_called_by_a_user_that_does_not_exist : Given_a_new_QuizzesController
    {
        private const int YEAR = 2012;
        private const int MONTH = 7;
        private const int DAY = 1;
        private Quiz quiz;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.UserConfigurations.GetByNameIdentifier()).Returns((UserConfiguration)null);
            this.quiz = this.QuizzesController.Get(YEAR, MONTH, DAY);
        }

        [Test]
        public void Then_the_UserId_should_be_0()
        {
            this.quiz.UserId.Should().Be(0);
        }
    }
}