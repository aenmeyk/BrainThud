using System.Collections.Generic;
using System.Linq;
using BrainThud.Web;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_GetForQuiz_is_called : Given_a_new_CardRepository
    {
        private IQueryable<Card> actualCards;

        private static IEnumerable<Card> cards
        {
            get
            {
                // Use Level=1 to indicate that a card should be included in results
                return new[]
                {
                    // Correct user, no matching QuizResult
                    new Card {PartitionKey = TestValues.CARD_PARTITION_KEY, RowKey = CardRowTypes.CARD + '-' + 0 },

                    // Correct user, matching QuizResult
                    new Card {PartitionKey = TestValues.CARD_PARTITION_KEY, RowKey = CardRowTypes.CARD + '-' + 1, Level = 1},

                    // Incorrect user, no matching QuizResult
                    new Card {PartitionKey = TestValues.PARTITION_KEY, RowKey = CardRowTypes.CARD + '-' + 2},

                    // Incorrect user, matching QuizResult
                    new Card {PartitionKey = TestValues.PARTITION_KEY, RowKey = CardRowTypes.CARD + '-' + 3},
                };
            }
        }

        private static IEnumerable<QuizResult> quizResults
        {
            get
            {
                return new[]
                {
                    new QuizResult { CardId = 1 },
                    new QuizResult { CardId = 3 },
                };
            }
        }

        public override void When()
        {
            this.CardKeyGenerator.Setup(x => x.GetRowKey(It.IsAny<int>())).Returns<int>(x => CardRowTypes.CARD + '-' + x);
            this.TableStorageContext.Setup(x => x.CreateQuery<Card>()).Returns(cards.AsQueryable());
            this.actualCards = this.CardRepository.GetForQuizResults(quizResults);
        }

        [Test]
        public void Then_all_Cards_for_the_user_are_returned_from_the_cards_repository()
        {
            this.actualCards.Should().HaveCount(1);
            this.actualCards.Should().OnlyContain(x => x.Level == 1);
        }
    }
}