using BrainThud.Core.Calendars;
using BrainThud.Core.Models;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;

namespace BrainThud.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly IIdentityQueueManager identityQueueManager;
        private readonly IQuizCalendar defaultQuizCalendar;

        public UserHelper(
            IAuthenticationHelper authenticationHelper, 
            IIdentityQueueManager identityQueueManager,
            IQuizCalendar defaultQuizCalendar)
        {
            this.authenticationHelper = authenticationHelper;
            this.identityQueueManager = identityQueueManager;
            this.defaultQuizCalendar = defaultQuizCalendar;
        }

        public UserConfiguration CreateUserConfiguration()
        {
            var userId = this.identityQueueManager.GetNextIdentity();
            var configurationId = this.identityQueueManager.GetNextIdentity();

            var configuration = new UserConfiguration
            {
                PartitionKey = string.Format("{0}-{1}", this.authenticationHelper.NameIdentifier, userId),
                RowKey = string.Format("{0}-{1}", CardRowTypes.CONFIGURATION, configurationId),
                UserId = userId,
                QuizInterval0 = defaultQuizCalendar[0],
                QuizInterval1 = defaultQuizCalendar[1],
                QuizInterval2 = defaultQuizCalendar[2],
                QuizInterval3 = defaultQuizCalendar[3],
                QuizInterval4 = defaultQuizCalendar[4],
                QuizInterval5 = defaultQuizCalendar[5]
            };

            return configuration;
        }
    }
}
