using BrainThud.Web.Controllers;
using BrainThudTest.Builders;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.FederationCallbackControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_end_as_the_id : Given_a_new_FederationCallbackController
    {
        private FederationCallbackController controller;
    
        public override void When()
        {
            this.controller = new ApiControllerBuilder<FederationCallbackController>(this.FederationCallbackController)
                .CreateRequest(this.Request)
                .Build();

            this.controller.Get("end");
        }

        [Test]
        public void Then_GetToken_should_not_be_called_on_the_TokenStore()
        {
            this.TokenStore.Verify(x => x.GetAndDeleteToken(It.IsAny<string>()), Times.Never());
        }
    }
}