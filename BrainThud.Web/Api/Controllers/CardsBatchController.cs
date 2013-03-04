﻿using System;
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
                    var originalCard = this.TableStorageContext.Cards.Get(card.PartitionKey, card.RowKey);
                    this.TableStorageContext.Detach(originalCard);

                    if(originalCard.DeckName != card.DeckName)
                    {
                        this.TableStorageContext.CardDecks.RemoveCardFromCardDeck(originalCard);
                        this.TableStorageContext.CardDecks.AddCardToCardDeck(card);
                    }

                    this.TableStorageContext.Cards.Update(card);
                }

                this.TableStorageContext.Commit();

                return this.Request.CreateResponse(HttpStatusCode.OK, cardList);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }
    }
}