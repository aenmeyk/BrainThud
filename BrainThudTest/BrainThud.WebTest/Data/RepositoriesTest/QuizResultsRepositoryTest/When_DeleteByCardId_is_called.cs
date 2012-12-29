using System.Linq;
using BrainThud.Web.Model;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.QuizResultsRepositoryTest
{
    [TestFixture]
    public class When_DeleteByCardId_is_called : Given_a_new_QuizResultsRepository
    {
        private const int MATCHING_QUIZ_RESULTS = 5;

        public override void When()
        {
            var quizResults = Builder<QuizResult>.CreateListOfSize(10)
                .TheFirst(MATCHING_QUIZ_RESULTS)
                    .With(x => x.CardId = TestValues.CARD_ID)
                    .And(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                    .And(x => x.RowKey = TestValues.QUIZ_RESULT_ROW_KEY)
                .Build();

            this.TableStorageContext.Setup(x => x.CreateQuery<QuizResult>()).Returns(quizResults.AsQueryable());
            this.QuizResultsRepository.DeleteByCardId(TestValues.CARD_ID);
        }

        [Test]
        public void Then_all_the_QuizResults_with_the_specified_CardId_are_deleted()
        {
            this.TableStorageContext.Verify(x => x.DeleteObject(It.Is<QuizResult>(qr => qr.CardId == TestValues.CARD_ID)), Times.Exactly(MATCHING_QUIZ_RESULTS));
        }
    }
}