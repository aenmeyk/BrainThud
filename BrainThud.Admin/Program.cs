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

            SetCardValues(context);
//            SetQuizResultValues(context);

             context.Commit();
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
                item.CardQuizDate = DateTime.UtcNow.AddDays(-1);
                context.QuizResults.Update(item);
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
