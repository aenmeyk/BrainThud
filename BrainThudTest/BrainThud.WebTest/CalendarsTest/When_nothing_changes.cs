using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.CalendarsTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_DefaultQuizCalendar
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_the_calendar_should_have_at_least_6_levels()
        {
            this.DefaultQuizCalendar.Count.Should().BeGreaterOrEqualTo(6);
        }

        [Test]
        public void Then_the_keys_should_be_in_ascending_order()
        {
            this.DefaultQuizCalendar.Keys.Should().BeInAscendingOrder();
        }

        [Test]
        public void Then_the_values_should_be_in_ascending_order()
        {
            this.DefaultQuizCalendar.Values.Should().BeInAscendingOrder();
        }
    }
}