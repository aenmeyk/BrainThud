using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ActionFiltersTest
{
    [TestFixture]
    public class When_OnActionExecuting_is_called_with_a_valid_ModelState : Given_a_new_ValidationActionFilter
    {
        public override void When()
        {
            this.ValidationActionFilter.OnActionExecuting(this.Context);
        }

        [Test]
        public void Then_the_HttpActionContext_Response_should_be_null()
        {
            this.Context.Response.Should().BeNull();
        }
    }
}