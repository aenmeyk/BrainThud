using System;
using System.Linq;
using System.Web.Mvc;
using BrainThud.Core;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Controllers
{
    public class CardsController : Controller
    {
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }

        public CardsController(ITableStorageContextFactory tableStorageContextFactory)
        {
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD));
        }

        //
        // GET: /Library/

        public ActionResult Index()
        {
            var cardDecks = this.TableStorageContext.CardDecks.GetAll().ToList();

#if !DEBUG
            cardDecks = cardDecks.Where(x => x.PartitionKey != ConfigurationSettings.TEST_PARTITION_KEY && x.PartitionKey != ConfigurationSettings.DEV_PARTITION_KEY).ToList();
#endif

            return View(cardDecks.OrderBy(x => x.DeckName));
        }

        //
        // GET: /Library/5/342

        public ActionResult Deck(int userId, string deckNameSlug)
        {
            // Shouldn't use slug because it could be duplicated
            deckNameSlug = deckNameSlug.ToLower();
            var userConfiguration = this.TableStorageContext.UserConfigurations.GetByUserId(userId);
            var partitionKey = userConfiguration.PartitionKey;
            var cards = this.TableStorageContext.Cards.Get(partitionKey)
                .Where(x => x.DeckNameSlug == deckNameSlug)
                .ToList();

            return View("card-deck", cards);
        }

        public ActionResult Card(int userId, int cardId)
        {
            var userConfiguration = this.TableStorageContext.UserConfigurations.GetByUserId(userId);
            var partitionKey = userConfiguration.PartitionKey;
            var card = this.TableStorageContext.Cards.GetByPartitionKey(partitionKey, cardId);
            return View("card", card);
        }
    }
}
