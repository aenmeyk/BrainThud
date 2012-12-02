﻿using BrainThud.Web.Data;
using BrainThud.Web.Model;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.EntitySetDictionaryTest
{
    [TestFixture]
    public class When_QuizResult_key_is_used : Given_a_new_EntitySetDictionary
    {
        private string result;

        public override void When()
        {
            this.result = this.EntitySetDictionary[typeof(QuizResult)];
        }

        [Test]
        public void Then_Card_is_returned()
        {
            this.result.Should().Be(EntitySetNames.CARD);
        }
    }
}