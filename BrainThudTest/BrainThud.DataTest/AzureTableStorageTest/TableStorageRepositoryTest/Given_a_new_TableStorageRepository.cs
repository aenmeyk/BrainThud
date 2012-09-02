﻿using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.AzureTableStorageTest.TableStorageRepositoryTest
{
    [TestFixture]
    public abstract class Given_a_new_TableStorageRepository_of_Nugget : Gwt
    {
        protected const string PARTITION_KEY = "FBF6E865-F0C6-4CE4-8185-653BA99F2F92";
        protected const string ROW_KEY = "FBF6E865-F0C6-4CE4-8185-653BA99F2F92";

        public override void Given()
        {
            var tableStorageGenerator = new Mock<ITableStorageKeyGenerator>();
            tableStorageGenerator.Setup(x => x.GeneratePartitionKey()).Returns(PARTITION_KEY);
            tableStorageGenerator.Setup(x => x.GenerateRowKey()).Returns(ROW_KEY);
            this.TableStorageContext = new Mock<ITableStorageContext<Nugget>>();
            this.TableStorageRepository = new TableStorageRepository<Nugget>(this.TableStorageContext.Object, tableStorageGenerator.Object);
        }

        protected Mock<ITableStorageContext<Nugget>> TableStorageContext { get; private set; }
        protected TableStorageRepository<Nugget> TableStorageRepository { get; private set; }
    }
}