using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using BrainThud.Web.Resources;

namespace BrainThud.Web.Controllers
{
    public class CardsController : ApiControllerBase
    {
        private readonly ITableStorageContextFactory tableStorageContextFactory;
        private readonly IAuthenticationHelper authenticationHelper;

        public CardsController(
            ITableStorageContextFactory tableStorageContextFactory,
            IAuthenticationHelper authenticationHelper)
        {
            this.tableStorageContextFactory = tableStorageContextFactory;
            this.authenticationHelper = authenticationHelper;
        }

        public IEnumerable<Card> Get()
        {
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
            return tableStorageContext.Cards.GetForUser();
        }

        public Card Get(int userId, int cardId)
        {
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
            var card = tableStorageContext.Cards.GetById(userId, cardId);
            if (card == null) throw new HttpException((int)HttpStatusCode.NotFound, ErrorMessages.The_specified_card_could_not_be_found);
               
            return card;
        }

        [ValidateInput(false)]
        public HttpResponseMessage Put(Card card)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
                tableStorageContext.Cards.Update(card);
                tableStorageContext.Commit();

                return this.Request.CreateResponse(HttpStatusCode.OK, card);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        [ValidateInput(false)]
        public HttpResponseMessage Post(Card card)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
                tableStorageContext.Cards.Add(card);
                tableStorageContext.CommitBatch();
                var response = this.Request.CreateResponse(HttpStatusCode.Created, card);

                var routeValues = new
                {
                    controller = this.ControllerContext.ControllerDescriptor.ControllerName,
                    userId = card.UserId,
                    cardId = card.EntityId
                };

                response.Headers.Location = new Uri(this.GetLink(RouteNames.API_CARDS, routeValues));
                return response;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        public HttpResponseMessage Delete(int userId, int cardId)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier);
                tableStorageContext.Cards.DeleteById(userId, cardId);
                tableStorageContext.QuizResults.DeleteByCardId(cardId);
                tableStorageContext.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }
    }
}