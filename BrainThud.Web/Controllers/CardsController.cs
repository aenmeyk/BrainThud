using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using BrainThud.Data;
using BrainThud.Model;
using BrainThud.Web.Resources;

namespace BrainThud.Web.Controllers
{
    public class CardsController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CardsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Card> Get()
        {
            return this.unitOfWork.Cards.GetAll();
        }

        public Card Get(string id)
        {
            try
            {
                return this.unitOfWork.Cards.Get(id);
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
                this.unitOfWork.Cards.Update(card);
                this.unitOfWork.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Post(Card card)
        {
            if (this.ModelState.IsValid)
            {
                this.unitOfWork.Cards.Add(card);
                this.unitOfWork.Commit();
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
                this.unitOfWork.Cards.Delete(id);
                this.unitOfWork.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}