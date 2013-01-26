using BrainThud.Web.Calendars;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Helpers;
using Moq;

namespace BrainThudTest.BrainThud.WebTest.HelpersTest.UserHelperTest
{
    public abstract class Given_a_new_UserHelper : Gwt
    {
        public override void Given()
        {
            this.IdentityQueueManager = new Mock<IIdentityQueueManager>();
            var authenticationHelper = new Mock<IAuthenticationHelper>();
            authenticationHelper.SetupGet(x => x.NameIdentifier).Returns(TestValues.NAME_IDENTIFIER);

            this.UserHelper = new UserHelper(
                authenticationHelper.Object, 
                this.IdentityQueueManager.Object,
                new DefaultQuizCalendar());
        }

        protected UserHelper UserHelper { get; private set; }
        protected Mock<IIdentityQueueManager> IdentityQueueManager { get; private set; }
    }
}