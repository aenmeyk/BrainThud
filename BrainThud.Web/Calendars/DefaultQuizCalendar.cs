namespace BrainThud.Web.Calendars
{
    public class DefaultQuizCalendar : QuizCalendar
    {
        public DefaultQuizCalendar()
        {
            this.Add(0, 1);
            this.Add(1, 6);
            this.Add(2, 24);
            this.Add(3, 66);
            this.Add(4, 114);
            this.Add(5, 246);
        }
    }
}