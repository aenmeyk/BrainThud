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
        // GET: /Library/Details/5

        //        public ActionResult Details(int id)
        //        {
        //            return View();
        //        }
    }
}
