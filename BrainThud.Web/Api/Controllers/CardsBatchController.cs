using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Api.Controllers
{
    public class CardsBatchController : ApiControllerBase
    {
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }

        public CardsBatchController(
            ITableStorageContextFactory tableStorageContextFactory,
            IAuthenticationHelper authenticationHelper)
        {
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, authenticationHelper.NameIdentifier));
        }

        public HttpResponseMessage Put(IEnumerable<Card> cards)
        {
            if(this.ModelState.IsValid)
            {
                var cardList = cards.ToList();

                foreach(var card in cardList)
                {
                    // TODO: This operation is done in multiple transactions so that duplicate card decks
                    // are not created.  Figure out a way to do this in a single transaction.
                    this.TableStorageContext.UpdateCardAndRelations(card);
                }

                this.TableStorageContext.Commit();
                return this.Request.CreateResponse(HttpStatusCode.OK, cardList);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }
    }
}