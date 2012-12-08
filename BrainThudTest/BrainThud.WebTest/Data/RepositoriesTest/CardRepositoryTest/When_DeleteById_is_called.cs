using BrainThud.Web.Model;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.RepositoriesTest.CardRepositoryTest
{
    [TestFixture]
    public class When_DeleteById_is_called : Given_a_new_CardRepository
    {
        public override void When()
        {
            this.CardRepository.DeleteById(TestValues.USER_ID, TestValues.CARD_ID);
        }

        [Test]
        public void Then_the_items_is_deleted_from_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => 
                x.DeleteObject(
                    It.Is<Card>(c => 
                        c.PartitionKey == TestValues.CARD_PARTITION_KEY 
                        && c.RowKey == TestValues.CARD_ROW_KEY)), 
                    Times.Once());
        }

        [Test]
        public void Then_no_other_items_are_deleted_from_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x =>
                x.DeleteObject(
                    It.Is<Card>(c =>
                        !(c.PartitionKey == TestValues.CARD_PARTITION_KEY
                        && c.RowKey == TestValues.CARD_ROW_KEY))),
                    Times.Never());
        }
    }
}