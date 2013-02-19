using System.Linq;
using BrainThud.Core;
using BrainThud.Core.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.UserConfigurationRepositoryTest
{
    [TestFixture]
    public class When_GetByUserId_is_called : Given_a_new_UserConfigurationRepository
    {
        private UserConfiguration userConfiguration;

        public override void When()
        {
            var randomGenerator = new RandomGenerator();
            var userConfigurations = Builder<UserConfiguration>.CreateListOfSize(5)
                .All().With(x => x.RowKey = string.Format("{0}-{1}", CardRowTypes.CONFIGURATION, randomGenerator.Next()))
                .Random(1).With(x => x.RowKey = TestValues.CONFIGURATION_ROW_KEY)
                .Build();

            this.TableStorageContext.Setup(x => x.CreateQuery<UserConfiguration>()).Returns(userConfigurations.AsQueryable());
            this.UserConfigurationKeyGenerator.Setup(x => x.GetRowKey(TestValues.CONFIGURATION_ID)).Returns(TestValues.CONFIGURATION_ROW_KEY);
            this.userConfiguration = this.UserConfigurationRepository.GetByUserId(TestValues.CONFIGURATION_ID);
        }

        [Test]
        public void Then_the_matching_UserConfiguration_is_returned()
        {
            this.userConfiguration.RowKey.Should().Be(TestValues.CONFIGURATION_ROW_KEY);
        }
    }
}