using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.CardKeyGeneratorTest
{
    [TestFixture]
    public abstract class Given_a_new_CardKeyGenerator : Gwt
    {
        protected const string USER_ROW_KEY = TestValues.ROW_KEY;
        protected const int CARD_ID = 8;

        public override void Given()
        {
            this.user = new User
            {
                RowKey = USER_ROW_KEY,
                Id = 5,
                LastUsedCardId = CARD_ID
            };

            this.UserRepository = new Mock<ITableStorageRepository<User>>();
            this.UserRepository.Setup(x => x.Get(USER_ROW_KEY)).Returns(this.user);
            var repositoryFactory = new Mock<IRepositoryFactory>();
            repositoryFactory.Setup(x => x.CreateTableStorageRepository<User>()).Returns(this.UserRepository.Object);
            this.CardKeyGenerator = new CardKeyGenerator(repositoryFactory.Object, USER_ROW_KEY);
        }

        protected User user { get; set; }
        protected Mock<ITableStorageRepository<User>> UserRepository { get; set; }
        protected CardKeyGenerator CardKeyGenerator { get; set; }
    }
}