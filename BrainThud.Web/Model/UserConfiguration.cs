using BrainThud.Core.Data.AzureTableStorage;
using BrainThud.Web.Calendars;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Model
{
    public class UserConfiguration : AzureTableEntity
    {
        public int UserId { get; set; }

        // TODO: Make this some sort of list or array
        public int QuizInterval0 { get; set; }
        public int QuizInterval1 { get; set; }
        public int QuizInterval2 { get; set; }
        public int QuizInterval3 { get; set; }
        public int QuizInterval4 { get; set; }
        public int QuizInterval5 { get; set; }

        public IQuizCalendar QuizCalendar
        {
            get
            {
                var calendar = new QuizCalendar
                {
                    { 0, this.QuizInterval0 },
                    { 1, this.QuizInterval1 },
                    { 2, this.QuizInterval2 },
                    { 3, this.QuizInterval3 },
                    { 4, this.QuizInterval4 },
                    { 5, this.QuizInterval5 }
                };

                return calendar;
            }
        }
    }
}