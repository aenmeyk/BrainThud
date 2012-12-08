using System.Net;
using System.Net.Http;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.QuizResultsControllerTest
{
    [TestFixture]
    public class When_Delete_is_called : Given_a_new_QuizResultsController
    {
        private HttpResponseMessage response;

        public override void When()
        {
            this.response = this.QuizResultsController.Delete(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_Delete_is_called_on_the_CardRepository()
        {
            this.TableStorageContext.Verify(x => x.QuizResults.Delete(TestValues.NAME_IDENTIFIER, TestValues.ROW_KEY), Times.Once());
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