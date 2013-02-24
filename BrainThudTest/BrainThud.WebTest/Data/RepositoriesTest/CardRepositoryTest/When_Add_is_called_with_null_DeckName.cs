﻿using BrainThud.Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_Add_is_called_with_null_values : Given_a_new_CardRepository
    {
        private readonly Card card = new Card();

        public override void When()
        {
            this.CardRepository.Add(this.card);
        }

        [Test]
        public void Then_the_DeckNameSlug_should_be_null()
        {
            this.card.DeckNameSlug.Should().Be(null);
        }

        [Test]
        public void Then_the_CardSlug_should_be_null()
        {
            this.card.CardSlug.Should().Be(null);
        }
    }
}