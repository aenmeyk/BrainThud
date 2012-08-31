using BrainThudTest.Extensions;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;
using FluentAssertions;

namespace BrainThudTest.BrainThud.ModelTest.NuggetTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_Nugget
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_Id_should_get_and_set_a_Guid()
        {
            this.Nugget.CanGetSetGuid(x => x.Id);
        }

        [Test]
        public void Then_Question_should_get_and_set_a_string()
        {
            this.Nugget.CanGetSetString(x => x.Question);
        }

        [Test]
        public void Then_Answer_should_get_and_set_a_string()
        {
            this.Nugget.CanGetSetString(x => x.Answer);
        }

        [Test]
        public void Then_AdditionalInformation_should_get_and_set_a_string()
        {
            this.Nugget.CanGetSetString(x => x.AdditionalInformation);
        }

        [Test]
        public void Then_Nugget_should_inherit_from_TableServiceEntity()
        {
            this.Nugget.Should().BeAssignableTo<TableServiceEntity>();
        }
    }
}