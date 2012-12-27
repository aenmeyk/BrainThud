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
        protected const int CALENDAR_DAYS = 10;
        protected const int CALENDAR_LEVEL = 2;

        public override void Given()
        {
            this.QuizCalendar = new Mock<IQuizCalendar>();
            this.QuizCalendar.Setup(x => x[CALENDAR_LEVEL]).Returns(CALENDAR_DAYS);
            this.QuizResultHandler = new QuizResultHandler(this.QuizCalendar.Object);
        }

        protected Mock<IQuizCalendar> QuizCalendar { get; private set; }
        protected QuizResultHandler QuizResultHandler { get; private set; }
    }
}