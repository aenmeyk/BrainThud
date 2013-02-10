using BrainThud.Core.Calendars;
using BrainThud.Core.Models;
using BrainThud.Web;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest.UserHelperTest
{
    [TestFixture]
    public class When_CreateUserConfiguration_is_called : Given_a_new_UserHelper
    {
        private UserConfiguration userConfiguration;
        private const int NEXT_IDENTITY_VALUE = 651;

        public override void When()
        {
            this.IdentityQueueManager.Setup(x => x.GetNextIdentity()).Returns(NEXT_IDENTITY_VALUE);
            this.userConfiguration = this.UserHelper.CreateUserConfiguration();
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_with_the_next_identity_value_in_the_PartitionKey()
        {
            this.userConfiguration.PartitionKey.Should().Be(TestValues.NAME_IDENTIFIER + "-" + NEXT_IDENTITY_VALUE);
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_with_the_next_identity_value_in_the_RowKey()
        {
            this.userConfiguration.RowKey.Should().Be(CardRowTypes.CONFIGURATION + "-" + NEXT_IDENTITY_VALUE);
        }

        [Test]
        public void Then_a_new_UserConfiguration_is_created_with_the_next_identity_value_as_the_UserId()
        {
            this.userConfiguration.UserId.Should().Be(NEXT_IDENTITY_VALUE);
        }

        [Test]
        public void Then_the_users_quiz_calendar_is_set_from_the_DefaultCalendar()
        {
            var defaultQuizCalendar = new DefaultQuizCalendar();
            this.userConfiguration.QuizCalendar[0].Should().Be(defaultQuizCalendar[0]);
            this.userConfiguration.QuizCalendar[1].Should().Be(defaultQuizCalendar[1]);
            this.userConfiguration.QuizCalendar[2].Should().Be(defaultQuizCalendar[2]);
            this.userConfiguration.QuizCalendar[3].Should().Be(defaultQuizCalendar[3]);
            this.userConfiguration.QuizCalendar[4].Should().Be(defaultQuizCalendar[4]);
            this.userConfiguration.QuizCalendar[5].Should().Be(defaultQuizCalendar[5]);
        }
    }
}