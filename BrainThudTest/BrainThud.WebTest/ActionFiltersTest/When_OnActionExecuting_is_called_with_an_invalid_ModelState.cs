using System.Net;
using System.Net.Http;
using BrainThudTest.Tools;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ActionFiltersTest
{
    [TestFixture]
    public class When_OnActionExecuting_is_called_with_an_invalid_ModelState : Given_a_new_ValidationActionFilter
    {
        public override void When()
        {
            this.Context.ModelState.AddModelError(TestValues.ERROR_KEY, TestValues.ERROR_MESSAGE);
            this.ValidationActionFilter.OnActionExecuting(this.Context);
        }

        [Test]
        public void Then_the_HttpActionContext_Response_should_be_null()
        {
            this.Context.Response.Should().NotBeNull();
        }

        [Test]
        public void Then_the_response_should_have_a_status_code_of_400()
        {
            this.Context.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Then_the_response_RequestMessage_should_not_be_null()
        {
            this.Context.Response.RequestMessage.Should().NotBeNull();
        }

        [Test]
        public void Then_the_model_error_should_be_returned_in_the_response_content()
        {
            var jObject = (JObject)((ObjectContent<JObject>)this.Context.Response.Content).Value;
            jObject[TestValues.ERROR_KEY].Value<string>().Should().Be(TestValues.ERROR_MESSAGE);
        }
    }
}