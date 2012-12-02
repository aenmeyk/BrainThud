using BrainThud.Web.Data.KeyGenerators;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.TableStorageKeyGeneratorTest
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