using System;
using System.Configuration;
using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.DependencyResolution;
using Microsoft.WindowsAzure;
using System.Linq;

namespace BrainThud.Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();

            var container = IoC.Initialize();
            var contextFactory = container.GetInstance<ITableStorageContextFactory>();
            var context = contextFactory.CreateTableStorageContext(AzureTableNames.CARD);

            SetCardValues(context);

            context.Commit();
        }

        private static void SetCardValues(ITableStorageContext context)
        {
            var cards = context.Cards.GetAll();
            var quizResults = context.QuizResults.GetAll().ToList();

            foreach (var item in cards)
            {
                var quizResult = quizResults
                    .Where(x => x.CardId == item.EntityId)
                    .OrderBy(x => x.QuizDate)
                    .LastOrDefault();

                if(quizResult != null)
                {
                    item.CompletedQuizYear = quizResult.QuizYear;
                    item.CompletedQuizMonth = quizResult.QuizMonth;
                    item.CompletedQuizDay = quizResult.QuizDay;
                    item.IsCorrect = quizResult.IsCorrect;
                }


                context.Cards.Update(item);
            }
        }

        private static void SetQuizResultValues(ITableStorageContext context)
        {
            var items = context.QuizResults.GetAll();

            foreach (var item in items)
            {
                context.QuizResults.Update(item);
            }
        }

        private static void SetConfigValues(ITableStorageContext context)
        {
            var items = context.UserConfigurations.GetAll();

            foreach (var item in items)
            {
                item.QuizInterval0 = 1;
                item.QuizInterval1 = 5;
                item.QuizInterval2 = 14;
                item.QuizInterval3 = 28;
                item.QuizInterval4 = 60;
                item.QuizInterval5 = 120;
                context.UserConfigurations.Update(item);
            }
        }

        static void Initialize()
        {
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                var connectionString = ConfigurationManager.AppSettings[configName];
                configSetter(connectionString);
            });
        }
    }
}
