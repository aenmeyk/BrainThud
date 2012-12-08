﻿using System;
using System.Web;
using BrainThud.Web.Resources;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_Get_is_called_with_an_Id_that_does_not_exist : Given_a_new_CardsController
    {
        public override void When()
        {
            this.TableStorageContext.Setup(x => x.Cards.Get(TestValues.NAME_IDENTIFIER, TestValues.CARD_ROW_KEY)).Throws(new InvalidOperationException(ErrorMessages.Sequence_contains_no_matching_element));
            this.CardsController.Get(TestValues.CARD_ID);
        }

        [Test]
        public void Then_an_HttpException_is_thrown()
        {
            this.ShouldThrowException<HttpException>(ErrorMessages.The_specified_card_could_not_be_found);
        }
    }
}