using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using BrainThud.Web.Data;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using BrainThud.Web.Resources;

namespace BrainThud.Web.Controllers
{
    public class CardsController : ApiControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthenticationHelper authenticationHelper;

        public CardsController(IUnitOfWork unitOfWork, IAuthenticationHelper authenticationHelper)
        {
            this.unitOfWork = unitOfWork;
            this.authenticationHelper = authenticationHelper;
        }

        public IEnumerable<Card> Get()
        {
            return this.unitOfWork.Cards.GetAll();
        }

        public Card Get(string id)
        {
            try
            {
                return this.unitOfWork.Cards.Get(this.authenticationHelper.NameIdentifier, id);
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
                this.unitOfWork.Cards.Delete(this.authenticationHelper.NameIdentifier, id);
                this.unitOfWork.Commit();

                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}