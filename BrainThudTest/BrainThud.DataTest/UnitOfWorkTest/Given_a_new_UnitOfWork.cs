﻿using BrainThud.Data;
using BrainThud.Data.AzureTableStorage;
using BrainThud.Model;
using BrainThudTest.Tools;
using Moq;
using NUnit.Framework;

namespace BrainThudTest.BrainThud.DataTest.UnitOfWorkTest
{
    [TestFixture]
    public abstract class Given_a_new_UnitOfWork : Gwt
    {
        public override void Given()
        {
            this.CardTableStorageRepository = new Mock<ITableStorageRepository<Card>>();
            this.QuizResultTableStorageRepository = new Mock<ITableStorageRepository<QuizResult>>();
            var repositoryFactory = new Mock<IRepositoryFactory>();
            repositoryFactory.Setup(x => x.CreateTableStorageRepository<Card>()).Returns(this.CardTableStorageRepository.Object);
            repositoryFactory.Setup(x => x.CreateTableStorageRepository<QuizResult>()).Returns(this.QuizResultTableStorageRepository.Object);
            this.UnitOfWork = new UnitOfWork(repositoryFactory.Object);
        }

        protected Mock<ITableStorageRepository<Card>>  CardTableStorageRepository { get; private set; }
        protected Mock<ITableStorageRepository<QuizResult>> QuizResultTableStorageRepository { get; private set; }
        protected UnitOfWork UnitOfWork { get; private set; }
    }
}