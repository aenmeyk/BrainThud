using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Builders;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageContextTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageContext_of_Nugget : Gwt
    {
        public override void Given()
        {
            var cloudStorageAccount = new CloudStorageAccountBuilder().Build();
            this.TableStorageContext = new TableStorageContext<Nugget>(EntitySetNames.NUGGET, cloudStorageAccount, createTable:false);
        }

        protected TableStorageContext<Nugget> TableStorageContext { get; private set; }
    }
}