using System.Web;
using BrainThud.Core.Models;
using BrainThud.Core.Models;
using BrainThud.Web.Resources;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_a_CardId_that_does_not_exist : Given_a_new_CardsController
    {
        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.GetById(TestValues.USER_ID, TestValues.CARD_ID)).Returns((Card)null);
            this.CardsController.Get(TestValues.USER_ID, TestValues.CARD_ID);
        }

        [Test]
        public void Then_an_HttpException_is_thrown()
        {
            this.ShouldThrowException<HttpException>(ErrorMessages.The_specified_card_could_not_be_found);
        }
    }
}