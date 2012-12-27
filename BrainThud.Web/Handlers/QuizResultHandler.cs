using System;
using System.Linq;
using BrainThud.Web.Calendars;
using BrainThud.Web.Data.AzureTableStorage;
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

        public void UpdateCardLevel(QuizResult quizResult, Card card)
        {
            card.QuizDate = DateTime.UtcNow.AddDays(this.quizCalendar[card.Level]).Date;
            card.Level = quizResult.IsCorrect
                ? card.Level + 1
                : 0;
        }

        public void ReverseIfExists(ITableStorageContext tableStorageContext, QuizResult quizResult, Card card)
        {
            var quizDate = quizResult.QuizDate;

// ReSharper disable ReplaceWithSingleCallToFirstOrDefault
            var existingResult = tableStorageContext.QuizResults
                .GetForQuiz(quizDate.Year, quizDate.Month, quizDate.Day)
                .Where(x => x.CardId == card.EntityId)
                .FirstOrDefault();
// ReSharper restore ReplaceWithSingleCallToFirstOrDefault

            if(existingResult != null)
            {
                if(card.Level > 0)
                {
                    card.Level--;
                    card.QuizDate = card.QuizDate.AddDays(-this.quizCalendar[card.Level]).Date;
                }
                
                tableStorageContext.QuizResults.Delete(existingResult.PartitionKey, existingResult.RowKey);
            }
        }
    }
}