using BrainThud.Data;
using BrainThud.Model;
using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public class When_Delete_is_called : Given_a_new_TableStorageRepository_of_Nugget
    {
        private readonly Nugget nuggetShoudBeDeleted = new Nugget { PartitionKey = Keys.TEMP_PARTITION_KEY, RowKey = TestValues.ROW_KEY };
        private readonly Nugget nuggetShoudNotBeDeleted = new Nugget { PartitionKey = Keys.TEMP_PARTITION_KEY, RowKey = "24D0E17E-99C6-458B-99E8-6878BA5A1E74" };

        public override void When()
        {
            this.TableStorageContext.Setup(x => x.CreateQuery(this.nuggetShoudBeDeleted.GetType().Name)).Returns(new[] { this.nuggetShoudBeDeleted, this.nuggetShoudNotBeDeleted });
            this.TableStorageRepository.Delete(TestValues.ROW_KEY);
        }

        [Test]
        public void Then_the_items_is_deleted_from_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.DeleteObject(this.nuggetShoudBeDeleted), Times.Once());
        }

        [Test]
        public void Then_no_other_items_are_deleted_from_the_TableStorageContext()
        {
            this.TableStorageContext.Verify(x => x.DeleteObject(this.nuggetShoudNotBeDeleted), Times.Never());
        }
    }
}