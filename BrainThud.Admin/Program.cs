using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web;
using BrainThud.Web.Api;
using BrainThud.Web.Api.Controllers;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.DependencyResolution;
using BrainThudTest;
using FizzWare.NBuilder;
using Microsoft.WindowsAzure;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using BrainThud.Web.Extensions;
using Microsoft.WindowsAzure.Storage.Auth.Protocol;
using StructureMap;
using System.Threading;

namespace BrainThud.Admin
{
    class Program
    {
        static void Main(string[] args)
        {
//            Initialize();

            var container = IoC.Initialize();
            var contextFactory = container.GetInstance<ITableStorageContextFactory>();
            var context = contextFactory.CreateTableStorageContext(AzureTableNames.CARD);

//            SetCardDecks(context);
            GenerateTestData(container);

//            context.Commit();
        }

        private static void SetCardValues(ITableStorageContext context)
        {
            var cards = context.Cards.GetAll();

            foreach (var item in cards)
            {
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

        private static void SetCardDecks(ITableStorageContext context)
        {
            var items = context.Cards.GetAll();

            foreach (var item in items)
            {
                context.CardDecks.AddCardToCardDeck(item);
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

        private static void GenerateTestData(IContainer container)
        {
//            var identity = new GenericIdentity("httpswwwgooglecomaccountso8ididaitoawn66whrug-vmzp4sx7ikz2px5njx5dbv2u");
//            var principal = new GenericPrincipal(identity, null);
//            Thread.CurrentPrincipal = principal;
//            HttpContext.Current.User = principal;
            new AzureInitializer().Initialize();

            var config = new HttpConfiguration { DependencyResolver = new StructureMapWebApiResolver(container), };
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("xml", "application/xml"));

            WebApiConfig.Configure(config);

            var server = new HttpServer(config);
            var client = new HttpClient(server);
            client.DefaultRequestHeaders.Add(HttpHeaders.X_CLIENT_DATE, DateTime.Now.ToLongDateString());
            client.DefaultRequestHeaders.Add(HttpHeaders.X_TEST, "true");

            var cards = Builder<Card>.CreateListOfSize(1000)
                .All()
                    .With(x => x.PartitionKey = null)
                    .And(x => x.RowKey = null)
                    .And(x => x.Timestamp = TypeValues.MIN_SQL_DATETIME)
                .TheFirst(100).With(x => x.DeckName = "Philosophy of science")
                .TheNext(100).With(x => x.DeckName = "Functional MRI Investigations of the Human Brain")
                .TheNext(100).With(x => x.DeckName = "Cellular Neurobiology")
                .TheNext(100).With(x => x.DeckName = "Expression and Purification of Enzyme Mutants")
                .TheNext(100).With(x => x.DeckName = "7.02J Introduction to Experimental Biology and Communication, 18, LAB, CI-M; Biology (GIR)")
                .TheNext(100).With(x => x.DeckName = "Studies in Musical Composition")
                .TheNext(100).With(x => x.DeckName = "Mechanics of Structures and Soils")
                .TheNext(100).With(x => x.DeckName = "Space Systems Development")
                .TheNext(100).With(x => x.DeckName = "16.90 Computational Methods in Aerospace Engineering")
                .TheNext(100).With(x => x.DeckName = "2.51 Intermediate Heat and Mass Transfer")
                .Build();

            foreach(var card in cards)
            {
                var response = client.PostAsJsonAsync(TestUrls.CARDS, card).Result;
                if (!response.IsSuccessStatusCode) break;

                Thread.Sleep(500);
            }
        }

//        static void Initialize()
//        {
//            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
//            {
//                var connectionString = ConfigurationManager.AppSettings[configName];
//                configSetter(connectionString);
//            });
//        }
    }
}
