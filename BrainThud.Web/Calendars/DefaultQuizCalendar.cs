namespace BrainThud.Web.Calendars
{
    public class DefaultQuizCalendar : QuizCalendar
    {
        public DefaultQuizCalendar()
        {
            this.Add(0, 1);
            this.Add(1, 5);
            this.Add(2, 14);
            this.Add(3, 28);
            this.Add(4, 60);
            this.Add(5, 120);
        }
    }
}