using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;
using BrainThud.Model;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.NuggetsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_an_Id_parameter : Given_a_new_NuggetController
    {
        private readonly Nugget expectedResult = new Nugget();
        private Nugget actualResult;

        public override void When()
        {
            this.NuggetRepository.Setup(x => x.Get(TestValues.ROW_KEY)).Returns(this.expectedResult);
            this.actualResult = this.NuggetsController.Get(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_a_Nugget_is_returned_from_the_nuggets_repository()
        {
            this.actualResult.Should().Be(this.expectedResult);
            this.NuggetRepository.Verify(x => x.Get(TestValues.ROW_KEY));
        }
    }
}