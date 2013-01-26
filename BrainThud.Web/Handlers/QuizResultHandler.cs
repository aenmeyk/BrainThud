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
            quizResult.CardLevel = card.Level >= 0 ? card.Level : 0;
            quizResult.CardQuizDate = card.QuizDate;
            card.Level = quizResult.IsCorrect
                ? card.Level + 1
                : 0;

            if(card.Level < 0) card.Level = 0;

            var daysQuizExtended = this.QuizCalendar.GetQuizInterval(card.Level);
            card.QuizDate = DateTime.UtcNow.AddDays(daysQuizExtended).Date;
        }

        public void ReverseQuizResult(QuizResult quizResult, Card card)
        {
            card.Level = quizResult.CardLevel;
            card.QuizDate = quizResult.CardQuizDate;
        }
    }
}