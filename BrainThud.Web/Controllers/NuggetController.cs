using System.Collections.Generic;
using System.Web.Http;
using BrainThud.Data;
using BrainThud.Model;

namespace BrainThud.Web.Controllers
{
    public class NuggetController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public NuggetController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Nugget> Get()
        {
            return this.unitOfWork.Nuggets.GetAll();
        }
    }
}