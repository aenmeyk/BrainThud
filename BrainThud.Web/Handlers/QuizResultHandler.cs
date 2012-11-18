using System;
using BrainThud.Model;
using BrainThud.Web.Calendars;

namespace BrainThud.Web.Handlers
{
    public class QuizResultHandler : IQuizResultHandler 
    {
        private readonly IQuizCalendar quizCalendar;

        public QuizResultHandler(IQuizCalendar quizCalendar)
        {
            this.quizCalendar = quizCalendar;
        }

        public void UpdateCardLevel(QuizResult quizResult, Card card)
        {
            card.Level = quizResult.IsCorrect
                ? card.Level + 1
                : 0;

            card.QuizDate = DateTime.UtcNow.AddDays(this.quizCalendar[card.Level]).Date;
        }
    }
}