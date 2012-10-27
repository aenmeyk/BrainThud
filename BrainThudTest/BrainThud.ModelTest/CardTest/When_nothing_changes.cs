using BrainThudTest.Extensions;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.ModelTest.CardTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_Card
    {
        public override void When()
        {
            // nothing changes
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
        public void Then_AdditionalInformation_should_get_and_set_a_string()
        {
            this.Card.CanGetSetString(x => x.AdditionalInformation);
        }

        [Test]
        public void Then_Card_should_inherit_from_TableServiceEntity()
        {
            this.Card.Should().BeAssignableTo<TableServiceEntity>();
        }
    }
}