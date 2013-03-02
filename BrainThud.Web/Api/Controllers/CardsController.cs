using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BrainThud.Core;
using BrainThud.Core.Models;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Resources;

namespace BrainThud.Web.Api.Controllers
{
    public class CardsController : ApiControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }

        public CardsController(
            ITableStorageContextFactory tableStorageContextFactory,
            IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier));
        }

        public Card Get(int userId, int cardId)
        {
            var card = this.TableStorageContext.Cards.GetById(userId, cardId);
            if (card == null) throw new HttpException((int)HttpStatusCode.NotFound, ErrorMessages.The_specified_card_could_not_be_found);

            return card;
        }

        public IEnumerable<Card> Get()
        {
            return this.TableStorageContext.Cards.GetForUser().ToList().OrderBy(x => x.CreatedTimestamp);
        }

        public IEnumerable<Card> GetForQuiz(int year, int month, int day)
        {
            var quizDate = new DateTime(year, month, day)
                .AddDays(1).Date
                .AddMilliseconds(-1);

            var quizResults = this.TableStorageContext.QuizResults.GetForQuiz(year, month, day).ToList();
            var userCards = this.TableStorageContext.Cards.GetForUser().Where(x => x.QuizDate <= quizDate).ToList();
            var quizResultCards = this.TableStorageContext.Cards.GetForQuizResults(quizResults);

            return userCards.Union(quizResultCards).ToList().OrderBy(x => x.CreatedTimestamp);
        }

        public IEnumerable<Card> GetForCardDeck(string deckNameSlug)
        {
            return this.TableStorageContext.Cards.GetForUser().Where(x => x.DeckNameSlug == deckNameSlug).ToList();
        }

        [ValidateInput(false)]
        public HttpResponseMessage Put(Card card)
        {
            if (this.ModelState.IsValid)
            {
                var originalCard = this.TableStorageContext.Cards.Get(card.PartitionKey, card.RowKey);
                this.TableStorageContext.Detach(originalCard);

                if (originalCard.DeckName != card.DeckName)
                {
                    this.TableStorageContext.CardDecks.RemoveCardFromCardDeck(originalCard);
                    this.TableStorageContext.CardDecks.AddCardToCardDeck(card);
                }

                this.TableStorageContext.Cards.Update(card);
                this.TableStorageContext.Commit();

                return this.Request.CreateResponse(HttpStatusCode.OK, card);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        [ValidateInput(false)]
        public HttpResponseMessage Post(Card card)
        {
            if (this.ModelState.IsValid)
            {
                var clientDateTime = this.GetClientDateTime();
                this.TableStorageContext.Cards.Add(card, clientDateTime);
                this.TableStorageContext.CardDecks.AddCardToCardDeck(card);
                this.TableStorageContext.CommitBatch();
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

        public HttpResponseMessage Delete(Card card)
        {
            if (this.ModelState.IsValid)
            {
                this.TableStorageContext.Cards.DeleteById(card.UserId, card.EntityId);
                this.TableStorageContext.CardDecks.RemoveCardFromCardDeck(card);
                this.TableStorageContext.QuizResults.DeleteByCardId(card.EntityId);
                this.TableStorageContext.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        private DateTime GetClientDateTime()
        {
            DateTimeOffset clientDateTime;
            var clientDateTimeString = string.Empty;
            var xClientDateHeader = this.Request.Headers.FirstOrDefault(x => x.Key == HttpHeaders.X_CLIENT_DATE);

            if (xClientDateHeader.Key != null) clientDateTimeString = xClientDateHeader.Value.FirstOrDefault();

            if (!DateTimeOffset.TryParse(clientDateTimeString, out clientDateTime))
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ErrorMessages.X_Client_Date_header_field_is_required);
                throw new HttpResponseException(httpResponseMessage);
            }

            return DateTime.Parse(new DateTime(clientDateTime.Ticks).ToString("o"));
        }
    }
}