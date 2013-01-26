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

        public void ApplyQuizResult(QuizResult quizResult, Card card)
        {
            // Set current card values on QuizResult so it can be reversed
            quizResult.CardLevel = card.Level >= 0 ? card.Level : 0;
            quizResult.CardQuizDate = card.QuizDate;
            quizResult.CardIsCorrect = card.IsCorrect;
            quizResult.CardCompletedQuizDate = card.CompletedQuizDate;

            card.Level = quizResult.IsCorrect
                ? card.Level + 1
                : 0;

            if(card.Level < 0) card.Level = 0;

            var daysQuizExtended = this.QuizCalendar.GetQuizInterval(card.Level);
            card.QuizDate = DateTime.UtcNow.AddDays(daysQuizExtended).Date;
            card.IsCorrect = quizResult.IsCorrect;
            card.CompletedQuizDate = DateTime.UtcNow.Date;
        }

        public void ReverseQuizResult(QuizResult quizResult, Card card)
        {
            card.Level = quizResult.CardLevel;
            card.QuizDate = quizResult.CardQuizDate;
            card.IsCorrect = quizResult.CardIsCorrect;
            card.CompletedQuizDate = quizResult.CardCompletedQuizDate;
        }
    }
}