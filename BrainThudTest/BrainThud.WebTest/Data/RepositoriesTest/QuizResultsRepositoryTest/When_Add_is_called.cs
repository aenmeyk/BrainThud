using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.QuizResultsRepositoryTest
{
    [TestFixture]
    public class When_Add_is_called : Given_a_new_QuizResultsRepository
    {
        private Mock<ICardEntityKeyGenerator> keyGenerator;
        private readonly QuizResult quizResult = new QuizResult();

        public override void When()
        {
            this.keyGenerator = new Mock<ICardEntityKeyGenerator>();
            this.keyGenerator.Setup(x => x.GeneratePartitionKey()).Returns(TestValues.PARTITION_KEY);
            this.keyGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.ROW_KEY);
            this.keyGenerator.SetupGet(x => x.GeneratedUserId).Returns(TestValues.USER_ID);
            this.keyGenerator.SetupGet(x => x.GeneratedEntityId).Returns(TestValues.QUIZ_RESULT_ID);

            this.QuizResultsRepository.Add(this.quizResult, this.keyGenerator.Object);
        }

        [Test]
        public void Then_entity_PartitionKey_should_be_set_using_the_key_generator()
        {
            this.quizResult.PartitionKey.Should().Be(TestValues.PARTITION_KEY);
        }

        [Test]
        public void Then_entity_RowKey_should_be_set_using_the_key_generator()
        {
            this.quizResult.RowKey.Should().Be(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_entity_UserId_should_be_set_using_the_key_generator()
        {
            this.quizResult.UserId.Should().Be(TestValues.USER_ID);
        }

        [Test]
        public void Then_entity_EntityId_should_be_set_using_the_key_generator()
        {
            this.quizResult.EntityId.Should().Be(TestValues.QUIZ_RESULT_ID);
        }

        [Test]
        public void Then_the_entity_should_be_added_to_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.AddObject(this.quizResult), Times.Once());
        }
    }
}