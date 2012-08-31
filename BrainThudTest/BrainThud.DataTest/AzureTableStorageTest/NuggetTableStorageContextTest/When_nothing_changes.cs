using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using FluentAssertions;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.NuggetTableStorageContextTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_NuggetTableStorageContext
    {
        public override void When()
        {
            // nothing changes
        }

        [Test]
        public void Then_ITableStorageContext_should_be_implemented()
        {
            this.NuggetTableStorageContext.Should().BeAssignableTo<ITableStorageContext<Nugget>>();
        }

        [Test]
        public void Then_it_should_be_assignable_to_TableServiceContext()
        {
            this.NuggetTableStorageContext.Should().BeAssignableTo<TableServiceContext>();
        }
    }
}