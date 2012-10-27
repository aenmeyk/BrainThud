﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BrainThud.Data;
using BrainThud.Model;
using BrainThud.Web.Resources;

namespace BrainThud.Web.Controllers
{
    public class CardsController : ApiController
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
                    throw new HttpException((int)HttpStatusCode.NotFound, ErrorMessages.The_specified_knowledge_card_could_not_be_found);
                }

                throw;
            }
        }

        public HttpResponseMessage Put(Card card)
        {
            this.unitOfWork.Cards.Update(card);
            this.unitOfWork.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Post(Card card)
        {
            this.unitOfWork.Cards.Add(card);
            this.unitOfWork.Commit();
            var response = this.Request.CreateResponse(HttpStatusCode.Created, card);

            var routeValues = new
            {
                controller = this.ControllerContext.ControllerDescriptor.ControllerName,
                id = card.RowKey
            };

            response.Headers.Location = new Uri(this.GetLink(RouteNames.DEFAULT_API, routeValues));
            return response;
        }

        public HttpResponseMessage Delete(string id)
        {
            this.unitOfWork.Cards.Delete(id);
            this.unitOfWork.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // Allows Url.Link to be faked for testing
        public virtual string GetLink(string routeName, object routeValues)
        {
            return Url.Link(routeName, routeValues);
        }
    }
}