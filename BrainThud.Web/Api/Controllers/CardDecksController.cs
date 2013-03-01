using System;
using System.Collections.Generic;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using System.Linq;

namespace BrainThud.Web.Api.Controllers
{
    public class CardDecksController :ApiControllerBase
    {
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }

        public CardDecksController(
            ITableStorageContextFactory tableStorageContextFactory,
            IAuthenticationHelper authenticationHelper)
        {
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, authenticationHelper.NameIdentifier));
        }

        public IEnumerable<CardDeck> Get()
        {
            return this.TableStorageContext.CardDecks.GetForUser().ToList().OrderBy(x => x.DeckName);
        }
    }
}