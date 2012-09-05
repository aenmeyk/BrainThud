using System;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using BrainThud.Web;
using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;
using BrainThud.Model;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetsControllerTest
{
    [TestFixture]
    [Category(TestTypes.LONG_RUNNING)]
    public class When_Post_is_called : Given_a_new_NuggetController
    {
        private Nugget nugget;
        private HttpResponseMessage response;

        public override void When()
        {
            this.nugget = new Nugget { RowKey = "1C81541E-5062-43F5-B63D-E07BD579FE79" };
            this.response = this.NuggetsController.Post(this.nugget);
        }

        [Test]
        public void Then_Add_is_called_on_Nugget_repository()
        {
            this.NuggetRepository.Verify(x => x.Add(this.nugget), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_UnitOfWork()
        {
            this.UnitOfWork.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_an_HttpResponseMessage_is_returned_with_a_201_status_code()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void Then_the_Nugget_should_be_returned_in_the_response()
        {
            var task = this.response.Content.ReadAsStringAsync();
            var nuggetInResponse = Json.Decode(task.Result);
            ((string)nuggetInResponse.RowKey).Should().Be(this.nugget.RowKey);
        }

        [Test]
        public void Then_the_location_should_be_created_from_the_nuggets_RowKey()
        {
            var type = this.NuggetsController.RouteValues.GetType();
            var propertyInfo = type.GetProperty("id");
            var id = propertyInfo.GetValue(this.NuggetsController.RouteValues, null);

            id.Should().Be(this.nugget.RowKey);
            this.NuggetsController.RouteName.Should().Be(RouteConfig.DEFAULT_API);
            this.response.Headers.Location.ToString().Should().Be(TestValues.LOCALHOST);
        }
    }
}