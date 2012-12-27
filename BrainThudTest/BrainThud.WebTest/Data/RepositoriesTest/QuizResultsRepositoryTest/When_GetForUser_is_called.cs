using System.Linq;
using BrainThud.Web;
using BrainThud.Web.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.QuizResultsRepositoryTest
{
    [TestFixture]
    public class When_GetForUser_is_called : Given_a_new_QuizResultsRepository
    {
        private IQueryable<QuizResult> actualQuizResults;

        public override void When()
        {
            var generator = new UniqueRandomGenerator();
            var quizResults = Builder<QuizResult>.CreateListOfSize(10)
                .TheFirst(6)
                    .With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                    .And(x => x.RowKey = CardRowTypes.CARD + "-" + generator.Next(1, 100))
                .TheLast(4)
                    .With(x => x.PartitionKey = TestValues.CARD_PARTITION_KEY)
                    .And(x => x.RowKey = CardRowTypes.QUIZ_RESULT + "-" + generator.Next(1, 100))
                .Build();

            this.TableStorageContext.Setup(x => x.CreateQuery<QuizResult>()).Returns(quizResults.AsQueryable());
            this.actualQuizResults = this.QuizResultsRepository.GetForUser();
        }

        [Test]
        public void Then_all_Cards_for_the_user_are_returned_from_the_cards_repository()
        {
            this.actualQuizResults.Should().HaveCount(4);
            this.actualQuizResults.Should().OnlyContain(x => x.PartitionKey == TestValues.CARD_PARTITION_KEY && x.RowKey.StartsWith(CardRowTypes.QUIZ_RESULT + "-"));
        }
    }
}