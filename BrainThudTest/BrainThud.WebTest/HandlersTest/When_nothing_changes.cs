using BrainThud.Web.Handlers;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_QuizResultHandler
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_QuizResultHandler_should_implement_IQuizResultHandler()
        {
            this.QuizResultHandler.Should().BeAssignableTo<IQuizResultHandler>();
        }
    }
}