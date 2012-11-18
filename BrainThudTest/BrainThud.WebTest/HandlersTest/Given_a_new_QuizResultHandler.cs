using BrainThud.Web.Calendars;
using BrainThud.Web.Handlers;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HandlersTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizResultHandler : Gwt
    {
        public override void Given()
        {
            this.QuizCalendar = new Mock<IQuizCalendar>();
            this.QuizResultHandler = new QuizResultHandler(this.QuizCalendar.Object);
        }

        protected Mock<IQuizCalendar> QuizCalendar { get; private set; }
        protected QuizResultHandler QuizResultHandler { get; private set; }
    }
}