using System.Linq;
using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.UserRepositoryTest
{
    [TestFixture]
    public class When_GetByNameIdentifier_is_called : Given_a_new_UserRepository
    {
        private UserConfiguration matchingUserConfiguration;
        private UserConfiguration userConfiguration;

        public override void When()
        {
            this.matchingUserConfiguration = new UserConfiguration { PartitionKey = TestValues.CARD_PARTITION_KEY, RowKey = EntityNames.CONFIGURATION };
            var nonMatchingUserConfiguration1 = new UserConfiguration { PartitionKey = "Non-matchingPartitionKey", RowKey = EntityNames.CONFIGURATION };
            var nonMatchingUserConfiguration2 = new UserConfiguration { PartitionKey = TestValues.CARD_PARTITION_KEY, RowKey = "Non-matchingRowKey" };
            var userConfigurations = new[] { this.matchingUserConfiguration, nonMatchingUserConfiguration1, nonMatchingUserConfiguration2 };
            this.TableStorageContext.Setup(x => x.CreateQuery<UserConfiguration>()).Returns(userConfigurations.AsQueryable());

            this.userConfiguration = this.UserRepository.GetByNameIdentifier();
        }

        [Test]
        public void Then_the_matching_UserConfiguration_is_returned()
        {
            this.userConfiguration.Should().Be(this.matchingUserConfiguration);
        }
    }
}