using System;
using System.Configuration;
using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.DependencyResolution;
using Microsoft.WindowsAzure;

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

//            SetQuizResultValues(context);

//            context.Commit();
        }

        private static void SetCardValues(ITableStorageContext context)
        {
            var cards = context.Cards.GetAll();

            foreach(var card in cards)
            {
                card.CompletedQuizDate = TypeValues.MIN_SQL_DATETIME;
                context.Cards.Update(card);
            }
        }

        private static void SetQuizResultValues(ITableStorageContext context)
        {
            var items = context.QuizResults.GetAll();

            foreach (var item in items)
            {
                item.QuizYear = item.QuizDate.Year;
                item.QuizMonth = item.QuizDate.Month;
                item.QuizDay = item.QuizDate.Day;

                if(item.CardCompletedQuizDate < new DateTime(2000, 1, 1)) item.CardCompletedQuizDate = DateTime.UtcNow.AddDays(-1);

                context.QuizResults.Update(item);
//                context.Commit();
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
