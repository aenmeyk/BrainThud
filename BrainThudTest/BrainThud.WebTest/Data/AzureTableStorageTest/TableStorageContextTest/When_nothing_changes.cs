using BrainThud.Web;
using BrainThud.Web.Data.AzureTableStorage;
using FluentAssertions;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public class When_nothing_changes : Given_a_new_TableStorageContext_for_the_Card_entity
    {
        public override void When() { }

        [Test]
        public void Then_ITableStorageContext_should_be_implemented()
        {
            this.TableStorageContext.Should().BeAssignableTo<ITableStorageContext>();
        }

        [Test]
        public void Then_it_should_be_assignable_to_TableServiceContext()
        {
            this.TableStorageContext.Should().BeAssignableTo<TableServiceContext>();
        }

        [Test]
        public void Then_IgnoreResourceNotFoundException_should_be_true()
        {
            this.TableStorageContext.IgnoreResourceNotFoundException.Should().BeTrue();
        }
    }
}