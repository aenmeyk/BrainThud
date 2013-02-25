using BrainThud.Core;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class QuizResultKeyGenerator : CardEntityKeyGenerator
    {
        public QuizResultKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            IIdentityQueueManager identityQueueManager)
            : base(authenticationHelper, identityQueueManager, CardRowTypes.QUIZ_RESULT){}
    }
}