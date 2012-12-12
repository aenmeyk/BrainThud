using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.QuizResultsRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_QuizResultsRepository : Gwt
    {

        public override void Given()
        {
            this.TableStorageContext = new Mock<ITableStorageContext>();
            this.QuizResultsRepository = new QuizResultsRepository(this.TableStorageContext.Object, TestValues.NAME_IDENTIFIER);
        }

        protected Mock<ITableStorageContext> TableStorageContext { get; private set; }
        protected QuizResultsRepository QuizResultsRepository { get; private set; }
    }
}