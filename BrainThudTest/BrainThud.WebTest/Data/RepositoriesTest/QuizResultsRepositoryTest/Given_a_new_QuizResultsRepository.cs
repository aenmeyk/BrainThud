using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.QuizResultsRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizResultsRepository : Gwt
    {

        public override void Given()
        {
            this.QuizResultKeyGenerator = new Mock<ICardEntityKeyGenerator>();
            this.QuizResultKeyGenerator.Setup(x => x.GeneratePartitionKey(TestValues.USER_ID)).Returns(TestValues.PARTITION_KEY);
            this.QuizResultKeyGenerator.Setup(x => x.GenerateRowKey()).Returns(TestValues.ROW_KEY);
            this.QuizResultKeyGenerator.SetupGet(x => x.GeneratedEntityId).Returns(TestValues.QUIZ_RESULT_ID);

            this.TableStorageContext = new Mock<ITableStorageContext> { DefaultValue = DefaultValue.Mock };
            var userConfiguration = new UserConfiguration { UserId = TestValues.USER_ID };
            this.TableStorageContext
                .Setup(x => x.UserConfigurations.GetByNameIdentifier())
                .Returns(userConfiguration);

            this.QuizResultsRepository = new QuizResultsRepository(
                this.TableStorageContext.Object,
                this.QuizResultKeyGenerator.Object,
                TestValues.NAME_IDENTIFIER);
        }

        protected Mock<ICardEntityKeyGenerator> QuizResultKeyGenerator { get; private set; }
        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected QuizResultsRepository QuizResultsRepository { get; private set; }
    }
}