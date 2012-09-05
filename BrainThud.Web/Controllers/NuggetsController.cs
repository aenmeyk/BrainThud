using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Data;
using BrainThud.Model;

namespace BrainThud.Web.Controllers
{
    public class NuggetsController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public NuggetsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ILinkProvider LinkProvider { get; set; }

        public IEnumerable<Nugget> Get()
        {
            return this.unitOfWork.Nuggets.GetAll();
        }

        public HttpResponseMessage Put(Nugget nugget)
        {
            this.unitOfWork.Nuggets.Update(nugget);
            this.unitOfWork.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Post(Nugget nugget)
        {
            this.unitOfWork.Nuggets.Add(nugget);
            this.unitOfWork.Commit();
            var response = this.Request.CreateResponse(HttpStatusCode.Created, nugget);
            var routeValues = new
            {
                controller = this.ControllerContext.ControllerDescriptor.ControllerName,
                id = nugget.RowKey
            };
            response.Headers.Location = new Uri(this.GetLink(RouteConfig.DEFAULT_API, routeValues));
            return response;
        }

        public virtual string GetLink(string routeName, object routeValues)
        {
            return Url.Link(routeName, routeValues);
        }
    }
}