using BrainThud.Web.Data.AzureTableStorage;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.WebTest.Data.AzureTableStorageTest.EntitySetDictionaryTest
{
    [TestFixture]
    public abstract class Given_a_new_EntitySetDictionary : Gwt
    {
        public override void Given()
        {
            this.EntitySetDictionary = new EntitySetDictionary();
        }

        protected EntitySetDictionary EntitySetDictionary { get; set; }
    }
}