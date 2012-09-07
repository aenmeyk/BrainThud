using System.Net.Http;
using BrainThudTest.Tools;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ActionFiltersTest
{
    [TestFixture]
    public class When_OnActionExecuting_is_called_with_two_model_errors : Given_a_new_ValidationActionFilter
    {
        public override void When()
        {
            this.Context.ModelState.AddModelError(TestValues.ERROR_KEY, TestValues.ERROR_MESSAGE);
            this.Context.ModelState.AddModelError(TestValues.ERROR_KEY_2, TestValues.ERROR_MESSAGE_2);
            this.ValidationActionFilter.OnActionExecuting(this.Context);
        }

        [Test]
        public void Then_two_model_errors_should_be_returned_in_the_response_content()
        {
            var jObject = (JObject)((ObjectContent<JObject>)this.Context.Response.Content).Value;
            jObject.Count.Should().Be(2);
        }

        [Test]
        public void Then_all_model_error_messages_should_be_returned_in_the_response_content()
        {
            var jObject = (JObject)((ObjectContent<JObject>)this.Context.Response.Content).Value;
            jObject[TestValues.ERROR_KEY].Value<string>().Should().Be(TestValues.ERROR_MESSAGE);
            jObject[TestValues.ERROR_KEY_2].Value<string>().Should().Be(TestValues.ERROR_MESSAGE_2);
        }
    }
}