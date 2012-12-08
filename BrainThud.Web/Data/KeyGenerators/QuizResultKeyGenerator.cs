using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class QuizResultKeyGenerator : CardEntityKeyGenerator
    {
        public QuizResultKeyGenerator(
            IAuthenticationHelper authenticationHelper,
            ITableStorageContext tableStorageContext,
            IUserHelper userHelper)
            : base(authenticationHelper, tableStorageContext, userHelper, CardRowTypes.QUIZ_RESULT) {}
    }
}