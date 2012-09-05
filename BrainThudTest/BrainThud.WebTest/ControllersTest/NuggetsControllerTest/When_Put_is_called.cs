using System.Net;
using System.Net.Http;
using BrainThud.Model;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetsControllerTest
{
    [TestFixture]
    public class When_Put_is_called : Given_a_new_NuggetController
    {
        private Nugget nugget;
        private HttpResponseMessage result;

        public override void When()
        {
            this.nugget = new Nugget();
            this.result = this.NuggetsController.Put(this.nugget);
        }

        [Test]
        public void Then_an_HttpResponseMessage_is_returned()
        {
            this.result.Should().BeAssignableTo<HttpResponseMessage>();
        }

        [Test]
        public void Then_the_returned_status_code_should_be_204()
        {
            this.result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public void Then_Update_should_be_called_on_the_Nugget_repository()
        {
            this.NuggetRepository.Verify(x => x.Update(this.nugget), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_UnitOfWork()
        {
            this.UnitOfWork.Verify(x => x.Commit(), Times.Once());
        }
    }
}