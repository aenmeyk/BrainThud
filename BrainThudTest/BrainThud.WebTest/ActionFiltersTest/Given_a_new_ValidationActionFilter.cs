using System.Net.Http;
using System.Web.Http.Controllers;
using BrainThud.Web.ActionFilters;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ActionFiltersTest
{
    [TestFixture]
    public abstract class Given_a_new_ValidationActionFilter : Gwt
    {
        public override void Given()
        {
            this.Context = new HttpActionContext();
            this.Context.ControllerContext = new ControllerContextBuilder()
              .CreateRequest(HttpMethod.Post, TestUrls.CARDS)
              .Build();

            this.ValidationActionFilter = new ValidationActionFilter();
        }

        protected ValidationActionFilter ValidationActionFilter { get; private set; }
        protected HttpActionContext Context { get; private set; }
    }
}