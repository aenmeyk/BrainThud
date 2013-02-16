using System.Net;
using System.Web.Http;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Delete_is_called_and_the_card_does_not_exist : Given_a_new_QuizResultsController
    {
        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.GetById(TestValues.USER_ID, TestValues.CARD_ID)).Returns((Card)null);
            this.QuizResultsController.Delete(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY, 
                TestValues.CARD_ID);
        }

        [Test]
        public void Then_an_HttpResponseException_should_be_thrown_and_404_should_be_returned_in_the_response()
        {
            this.TestException<HttpResponseException>(x => x.Response.StatusCode.Should().Be(HttpStatusCode.NotFound));
        }
    }
}