using System;
using BrainThud.Web.Calendars;
using BrainThud.Web.Model;

namespace BrainThud.Web.Handlers
{
    public class QuizResultHandler : IQuizResultHandler
    {
        private readonly IQuizCalendar quizCalendar;

        public QuizResultHandler(IQuizCalendar quizCalendar)
        {
            this.quizCalendar = quizCalendar;
        }

        public void IncrementCardLevel(QuizResult quizResult, Card card)
        {
            card.Level = quizResult.IsCorrect
                ? card.Level + 1
                : 0;

            card.QuizDate = DateTime.UtcNow.AddDays(this.quizCalendar[card.Level]).Date;
        }

        public void DecrementCardLevel(Card card)
        {
            if (card.Level == 0) return;
            card.Level--;
            card.QuizDate = card.QuizDate.AddDays(-this.quizCalendar[card.Level]).Date;
        }
    }
}