using System.Linq;
using BrainThud.Data;
using BrainThud.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Delete_is_called : Given_a_new_TableStorageRepository_of_Card
    {
        private readonly Card cardShoudBeDeleted = new Card { PartitionKey = Keys.TEMP_PARTITION_KEY, RowKey = TestValues.ROW_KEY };
        private readonly Card cardShoudNotBeDeleted = new Card { PartitionKey = Keys.TEMP_PARTITION_KEY, RowKey = "24D0E17E-99C6-458B-99E8-6878BA5A1E74" };

        public override void When()
        {
            var cards = new[] { this.cardShoudBeDeleted, this.cardShoudNotBeDeleted }.AsQueryable();
            this.TableStorageContext.Setup(x => x.CreateQuery(this.cardShoudBeDeleted.GetType().Name)).Returns(cards);
            this.TableStorageRepository.Delete(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_items_is_deleted_from_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.DeleteObject(this.cardShoudBeDeleted), Times.Once());
        }

        [Test]
        public void Then_no_other_items_are_deleted_from_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.DeleteObject(this.cardShoudNotBeDeleted), Times.Never());
        }
    }
}