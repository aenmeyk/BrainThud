using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using BrainThud.Web.Data;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using BrainThud.Web.Resources;
using System.Linq;

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
            var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);
            return tableStorageContext.Cards.GetAll();
        }

        public Card Get(string id)
        {
            try
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);
                return tableStorageContext.Cards.Get(this.authenticationHelper.NameIdentifier, id);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == ErrorMessages.Sequence_contains_no_matching_element)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, ErrorMessages.The_specified_card_could_not_be_found);
                }

                throw;
            }
        }

        public HttpResponseMessage Put(Card card)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);
                tableStorageContext.Cards.Update(card);
                tableStorageContext.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Post(Card card)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);
                var keyGenerator = new CardKeyGenerator(this.authenticationHelper, tableStorageContext);
                tableStorageContext.Cards.Add(card, keyGenerator);
                tableStorageContext.Commit();
                var response = this.Request.CreateResponse(HttpStatusCode.Created, card);

                var routeValues = new
                {
                    controller = this.ControllerContext.ControllerDescriptor.ControllerName,
                    id = card.RowKey
                };

                response.Headers.Location = new Uri(this.GetLink(RouteNames.API_DEFAULT, routeValues));
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(string id)
        {
            if (this.ModelState.IsValid)
            {
                var tableStorageContext = this.tableStorageContextFactory.CreateTableStorageContext(EntitySetNames.CARD);
                tableStorageContext.Cards.Delete(this.authenticationHelper.NameIdentifier, id);
                tableStorageContext.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}