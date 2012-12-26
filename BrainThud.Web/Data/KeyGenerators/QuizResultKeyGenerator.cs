using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;

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