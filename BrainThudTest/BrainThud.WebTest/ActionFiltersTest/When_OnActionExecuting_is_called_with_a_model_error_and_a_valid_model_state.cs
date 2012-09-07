using System.Net.Http;
using System.Web.Http.ModelBinding;
using BrainThudTest.Tools;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ActionFiltersTest
{
    [TestFixture]
    public class When_OnActionExecuting_is_called_with_a_model_error_and_a_valid_model_state : Given_a_new_ValidationActionFilter
    {
        public override void When()
        {
            this.Context.ModelState.AddModelError(TestValues.ERROR_KEY, TestValues.ERROR_MESSAGE);
            this.Context.ModelState.Add(TestValues.VALID, new ModelState());
            this.ValidationActionFilter.OnActionExecuting(this.Context);
        }

        [Test]
        public void Then_one_model_errors_should_be_returned_in_the_response_content()
        {
            var jObject = (JObject)((ObjectContent<JObject>)this.Context.Response.Content).Value;
            jObject.Count.Should().Be(1);
        }
    }
}