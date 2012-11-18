using BrainThud.Web.Calendars;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.CalendarsTest
{
    [TestFixture]
    public abstract class Given_a_new_DefaultQuizCalendar : Gwt
    {
        public override void Given()
        {
            this.DefaultQuizCalendar = new DefaultQuizCalendar();
        }

        protected DefaultQuizCalendar DefaultQuizCalendar { get; private set; }
    }
}