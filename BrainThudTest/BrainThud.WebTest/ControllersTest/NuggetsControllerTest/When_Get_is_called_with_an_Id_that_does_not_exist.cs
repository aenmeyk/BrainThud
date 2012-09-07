using System;
using System.Web;
using BrainThud.Web.Resources;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_an_Id_that_does_not_exist : Given_a_new_NuggetController
    {
        public override void When()
        {
            this.NuggetRepository.Setup(x => x.Get(TestValues.ROW_KEY)).Throws(new InvalidOperationException(ErrorMessages.Sequence_contains_no_matching_element));
            this.NuggetsController.Get(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_an_HttpException_is_thrown()
        {
            this.ShouldThrowException<HttpException>(ErrorMessages.The_specified_knowledge_nugget_could_not_be_found);
        }
    }
}