using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using FluentAssertions;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_TableStorageContext_of_Nugget
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_ITableStorageContext_should_be_implemented()
        {
            this.TableStorageContext.Should().BeAssignableTo<ITableStorageContext<Nugget>>();
        }

        [Test]
        public void Then_it_should_be_assignable_to_TableServiceContext()
        {
            this.TableStorageContext.Should().BeAssignableTo<TableServiceContext>();
        }
    }
}