using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest.UserHelperTest
{
    [TestFixture]
    public class When_CreateUserConfiguration_is_called : Given_a_new_UserHelper
    {
        private const int NEXT_IDENTITY_VALUE = 651;

        public override void When()
        {
            this.IdentityQueueManager.Setup(x => x.GetNextIdentity()).Returns(NEXT_IDENTITY_VALUE);
            this.UserHelper.CreateUserConfiguration();
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_with_the_next_identity_value()
        {
            this.TableStorageContext.Verify(x => x.UserConfigurations.Add(
                It.Is<UserConfiguration>(c =>
                    c.PartitionKey == TestValues.NAME_IDENTIFIER + "-" + NEXT_IDENTITY_VALUE.ToString()
                    && c.RowKey == EntityNames.CONFIGURATION)), Times.Once());
        }

        [Test]
        public void Then_Commit_is_called_on_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.Commit(), Times.Once());
        }
    }
}