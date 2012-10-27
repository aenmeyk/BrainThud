﻿using System.Web.Http;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.ControllersTest.CardControllerTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_CardController
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_the_CardController_should_inherit_from_ApiController()
        {
            this.CardsController.Should().BeAssignableTo<ApiController>();
        }
    }
}