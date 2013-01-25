using System.Net;
using System.Net.Http;
using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.ConfigControllerTest
{
    [TestFixture]
    public class When_Put_is_called : Given_a_new_ConfigController
    {
        private HttpResponseMessage response;
        private UserConfiguration userConfiguration;

        public override void When()
        {
            this.userConfiguration = new UserConfiguration();
            this.TableStorageContext
                .Setup(x => x.UserConfigurations.Update(this.userConfiguration))
                .Callback<UserConfiguration>(x => x.UserId = TestValues.USER_ID);

            this.response = this.ConfigController.Put(this.userConfiguration);
        }

        [Test]
        public void Then_an_HttpResponseMessage_is_returned()
        {
            this.response.Should().BeAssignableTo<HttpResponseMessage>();
        }

        [Test]
        public void Then_the_returned_status_code_should_be_200()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Then_the_updated_UserConfiguration_should_be_returned_in_the_response()
        {
            this.response.Content.As<ObjectContent>().Value.As<UserConfiguration>().UserId.Should().Be(TestValues.USER_ID);
        }

        [Test]
        public void Then_Update_should_be_called_on_the_UserConfiguration_repository()
        {
            this.TableStorageContext.Verify(x => x.UserConfigurations.Update(this.userConfiguration), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }
    }
}