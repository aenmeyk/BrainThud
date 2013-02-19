using System;
using System.Linq;
using System.Web.Mvc;
using BrainThud.Core.Models;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Controllers
{
    public class LibraryController : Controller
    {
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }

        public LibraryController(ITableStorageContextFactory tableStorageContextFactory)
        {
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD));
        }

        //
        // GET: /Library/

#if !DEBUG
        [OutputCache(Duration = 3600)]
#endif
        public ActionResult Index()
        {
            var cards = this.TableStorageContext
                .Cards
                .GetAll()
                .ToList();

            var cardDecks = cards
                .Select(x => x.DeckName)
                .Distinct()
                .OrderBy(x => x)
                .Select(x => new CardDeck { DeckName = x });

            return View(cardDecks);
        }

        //
        // GET: /Library/5/342

        public ActionResult Deck(int userId, string deckNameSlug)
        {
            deckNameSlug = deckNameSlug.ToLower();
            var userConfiguration = this.TableStorageContext.UserConfigurations.GetByUserId(userId);
            var partitionKey = userConfiguration.PartitionKey;
            var cards = this.TableStorageContext.Cards.Get(partitionKey)
                .Where(x => x.DeckNameSlug == deckNameSlug)
                .ToList();

            return View("card-deck", cards);
        }
    }
}
