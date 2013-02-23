using System.Web.Mvc;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.LibraryControllerTest
{
    [TestFixture]
    public class When_Card_is_called : Given_a_new_LibraryController
    {
        private ViewResult result;
        private readonly Card card = new Card();

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.GetByPartitionKey(TestValues.PARTITION_KEY, TestValues.CARD_ID)).Returns(this.card);
            this.result = (ViewResult)this.LibraryController.Card(TestValues.USER_ID, TestValues.CARD_ID);
        }

        [Test]
        public void Then_the_card_View_is_returned()
        {
            this.result.ViewName.Should().Be("card");
        }

        [Test]
        public void Then_the_view_model_should_be_of_type_Card()
        {
            this.result.Model.Should().BeAssignableTo<Card>();
        }

        [Test]
        public void Then_the_card_should_be_returned_from_the_TableStorageContext()
        {
            this.result.Model.Should().Be(this.card);
        }
    }
}