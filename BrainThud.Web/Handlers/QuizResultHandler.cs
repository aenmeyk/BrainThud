using System;
using BrainThud.Web.Calendars;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

namespace BrainThud.Web.Handlers
{
    public class QuizResultHandler : IQuizResultHandler
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly Lazy<IQuizCalendar> lazyQuizCalendar;
        private IQuizCalendar QuizCalendar { get { return this.lazyQuizCalendar.Value; } }

        public QuizResultHandler(ITableStorageContextFactory tableStorageContextFactory, IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
            this.lazyQuizCalendar = new Lazy<IQuizCalendar>(() => tableStorageContextFactory
                .CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier)
                .UserConfigurations.GetByNameIdentifier().QuizCalendar);
        }

        public void IncrementCardLevel(QuizResult quizResult, Card card)
        {
            card.Level = quizResult.IsCorrect
                ? card.Level + 1
                : 0;

            card.QuizDate = DateTime.UtcNow.AddDays(this.QuizCalendar[card.Level]).Date;
        }

        public void DecrementCardLevel(Card card)
        {
            if (card.Level == 0) return;

            card.Level--;
            card.QuizDate = card.QuizDate.AddDays(-this.QuizCalendar[card.Level]).Date;
        }
    }
}