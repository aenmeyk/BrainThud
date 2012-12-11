using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class QuizResultKeyGenerator : CardEntityKeyGenerator
    {
        public QuizResultKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            ITableStorageContext tableStorageContext,
            IUserHelper userHelper,
            IIdentityQueueManager identityQueueManager)
            : base(authenticationHelper, tableStorageContext, userHelper, identityQueueManager, CardRowTypes.QUIZ_RESULT) {}
    }
}