using BrainThudTest.Extensions;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Model.CardTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_Card
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_DeckName_should_get_and_set_a_string()
        {
            this.Card.CanGetSetString(x => x.DeckName);
        }

        [Test]
        public void Then_Tags_should_get_and_set_a_string()
        {
            this.Card.CanGetSetString(x => x.Tags);
        }

        [Test]
        public void Then_Question_should_get_and_set_a_string()
        {
            this.Card.CanGetSetString(x => x.Question);
        }

        [Test]
        public void Then_Answer_should_get_and_set_a_string()
        {
            this.Card.CanGetSetString(x => x.Answer);
        }

        [Test]
        public void Then_QuizDate_should_get_and_set_a_DateTime()
        {
            this.Card.CanGetSetDateTime(x => x.QuizDate);
        }

        [Test]
        public void Then_Level_should_get_and_set_an_int()
        {
            this.Card.CanGetSetInt(x => x.Level);
        }

        [Test]
        public void Then_Card_should_inherit_from_TableServiceEntity()
        {
            this.Card.Should().BeAssignableTo<TableServiceEntity>();
        }
    }
}