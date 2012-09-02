using BrainThud.Data.AzureTableStorage;
using BrainThudTest.Tools;
using FluentAssertions;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.KeyGeneratorTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageKeyGenerator : Gwt
    {
        public override void Given()
        {
            this.TableStorageKeyGenerator = new TableStorageKeyGenerator();
        }

        protected TableStorageKeyGenerator TableStorageKeyGenerator { get; private set; }
    }
}