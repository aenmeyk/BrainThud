using System.Collections.Generic;
using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ApiTest.ControllersTest.CardsControllerTest
{
    [TestFixture]
    public class When_GetForQuiz_is_called_by_a_user_that_does_not_exist : Given_a_new_CardsController
    {
        private IEnumerable<Card> cards;

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.UserConfigurations.GetByNameIdentifier()).Returns((UserConfiguration)null);
            this.cards = this.CardsController.GetForQuiz(TestValues.YEAR, TestValues.MONTH, TestValues.DAY);
        }

        [Test]
        public void Then_the_UserId_should_be_an_empty_list()
        {
            this.cards.Should().HaveCount(0);
        }
    }
}