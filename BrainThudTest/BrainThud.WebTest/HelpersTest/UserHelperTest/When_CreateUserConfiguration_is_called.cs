using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest.UserHelperTest
{
    [TestFixture]
    public class When_CreateUserConfiguration_is_called : Given_a_new_UserHelper
    {
        private UserConfiguration result;
        private const int NEXT_IDENTITY_VALUE = 651;

        public override void When()
        {
            this.IdentityQueueManager.Setup(x => x.GetNextIdentity()).Returns(NEXT_IDENTITY_VALUE);
            this.result = this.UserHelper.CreateUserConfiguration();
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_with_the_next_identity_value_in_the_PartitionKey()
        {
            this.result.PartitionKey.Should().Be(TestValues.NAME_IDENTIFIER + "-" + NEXT_IDENTITY_VALUE);
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_with_the_next_identity_value_in_the_RowKey()
        {
            this.result.RowKey.Should().Be(CardRowTypes.CONFIGURATION + "-" + NEXT_IDENTITY_VALUE);
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_with_the_next_identity_value_as_the_UserId()
        {
            this.result.UserId.Should().Be(NEXT_IDENTITY_VALUE);
        }
    }
}