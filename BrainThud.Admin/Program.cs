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
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
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
            const string LONG_QUESTION = @"1. An anonymous user requests a protected resource
 2. In the `AuthorizeRequest` step, the `WSFederationAuthenticationModule` determines that the user is not authorized to access the resource and returns a `401 Not Authorized`
 3. In the `EndRequest` step, the `401` is changed to a `302 Redirect` to the Security Token Service
 4. The Security Token Service authenticates the user and returns a security token
 5. In the `AuthenticateRequest` step, the token is validated, claims transformation takes place and a session is established with a `SessionSecurityToken` as a session cookie
 6. Once the session has been established, the `SessionSecurityToken` is sent with each request and the `SessionAuthenticationModule` validates the `SessionSecurityToken` in the `AuthenticateRequest` step";

            const string LONG_ANSWER = @" 1. `ClaimsAuthenticationManager` - Reject requests based on missing or invalid identity information
 2. URL authorization module - Specify authorization elements in the `Web.config` files
   
        <location path='staff'>
            <system.web>
                <authorization>
                    <allow roles='Marketing' />
                    <deny users='*' />
                </authorization>
            </system.web>
        </location>
 3. `ClaimsAuthorizationModule` - Also uses the URL but is claims-based.

        <modules runAllManagedModulesForAllRequests='true'>
            <add name='ClaimsAuthorizationModule' 
                 type=System.IdentityModel.Services.ClaimsAuthorizationModule, ...' />
        </modules>

        <system.identityModel>
           <identityConfiguration>
               <claimsAuthorizationManager type='AuthorizationManager, ...' />
           <identityConfiguration>
        </system.identityModel>";


            new AzureInitializer().Initialize();

            var config = new HttpConfiguration { DependencyResolver = new StructureMapWebApiResolver(container), };
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("xml", "application/xml"));

            WebApiConfig.Configure(config);

            var server = new HttpServer(config);
            var client = new HttpClient(server);
            client.DefaultRequestHeaders.Add(HttpHeaders.X_CLIENT_DATE, DateTime.Now.ToLongDateString());
            client.DefaultRequestHeaders.Add(HttpHeaders.X_TEST, "true");

            var randomizer = new RandomGenerator();

            var namer = new RandomValuePropertyNamer(new RandomGenerator(), 
                                            new ReflectionUtil(), 
                                            true, 
                                            DateTime.Now, 
                                            DateTime.Now.AddDays(10), 
                                            true);

            BuilderSetup.SetDefaultPropertyNamer(namer);

            var cards = Builder<Card>.CreateListOfSize(10000)
                .All()
                    .With(x => x.PartitionKey = null)
                    .And(x => x.RowKey = null)
                    .And(x => x.Timestamp = TypeValues.MIN_SQL_DATETIME)
                .TheFirst(1000)
                    .With(x => x.DeckName = "Foundations of Medicine I (large)")
                    .And(x => x.Question = LONG_QUESTION)
                    .And(x => x.Answer = LONG_ANSWER)
                .TheNext(1000).With(x => x.DeckName = "Foundations of Medicine II")
                .TheNext(1000).With(x => x.DeckName = "Human Health & Disease II - Pulmonary")
                .TheNext(1000).With(x => x.DeckName = "Human Health & Disease II - Cardiovascular")
                .TheNext(1000).With(x => x.DeckName = "Practice of Medicine III")
                .TheNext(1000).With(x => x.DeckName = "Human Health & Disease III - Gastrointestinal")
                .TheNext(1000).With(x => x.DeckName = "Human Health & Disease IV - Multi-systemic Diseases")
                .TheNext(1000).With(x => x.DeckName = "Analysis of Structures")
                .TheNext(1000).With(x => x.DeckName = "Aircraft and Rocket Propulsion")
                .TheNext(1000).With(x => x.DeckName = "Multivariable Feedback Systems")
                .Build();

            foreach(var card in cards)
            {
                var response = client.PostAsJsonAsync(TestUrls.CARDS, card).Result;
                if (!response.IsSuccessStatusCode) break;

                Thread.Sleep(200);
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
