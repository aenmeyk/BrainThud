using System.Linq;
using System.Net;
using System.Net.Http;
using BrainThud.Core.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Delete_is_called : Given_a_new_QuizResultsController
    {
        private HttpResponseMessage response;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.QuizResults.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY))
                .Returns(new[] { new QuizResult { EntityId = TestValues.QUIZ_RESULT_ID, CardId = TestValues.CARD_ID } }.AsQueryable());

            this.response = this.QuizResultsController.Delete(
                TestValues.USER_ID, 
                TestValues.YEAR, 
                TestValues.MONTH, 
                TestValues.DAY, 
                TestValues.CARD_ID);
        }

        [Test]
        public void Then_Delete_is_called_on_the_CardRepository()
        {
            this.TableStorageContext.Verify(x => x.QuizResults.DeleteById(TestValues.USER_ID, TestValues.QUIZ_RESULT_ID), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void Then_the_return_status_code_should_be_204()
        {
            this.response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}