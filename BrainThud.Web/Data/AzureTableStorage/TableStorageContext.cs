using System;
using System.Data.Services.Client;
using System.Linq;
using BrainThud.Core.Models;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using Microsoft.WindowsAzure.Storage.Table.DataServices;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContext : TableServiceContext, ITableStorageContext
    {
        private readonly ICardEntityKeyGenerator cardKeyGenerator;
        private readonly ICardEntityKeyGenerator cardDeckKeyGenerator;
        private readonly ICardEntityKeyGenerator quizResultKeyGenerator;
        private readonly ICardEntityKeyGenerator userConfigurationKeyGenerator;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly string tableName;
        private readonly string nameIdentifier;
        private readonly Lazy<ICardRepository> cards;
        private readonly Lazy<ICardDeckRepository> cardDecks;
        private readonly Lazy<IQuizResultsRepository> quizResults;
        private readonly Lazy<IUserConfigurationRepository> userConfigurations;
        private readonly Lazy<ITableStorageRepository<MasterConfiguration>> masterConfigurations;

        public TableStorageContext(
            ICloudStorageServices cloudStorageServices,
            ICardEntityKeyGenerator cardKeyGenerator,
            ICardEntityKeyGenerator cardDeckKeyGenerator,
            ICardEntityKeyGenerator quizResultKeyGenerator,
            ICardEntityKeyGenerator userConfigurationKeyGenerator,
            IRepositoryFactory repositoryFactory,
            string tableName,
            string nameIdentifier)
            : base(cloudStorageServices.CloudTableClient)
        {
            this.cardKeyGenerator = cardKeyGenerator;
            this.cardDeckKeyGenerator = cardDeckKeyGenerator;
            this.quizResultKeyGenerator = quizResultKeyGenerator;
            this.userConfigurationKeyGenerator = userConfigurationKeyGenerator;
            this.repositoryFactory = repositoryFactory;
            this.tableName = tableName;
            this.nameIdentifier = nameIdentifier;
            this.IgnoreResourceNotFoundException = true;
            this.cards = new Lazy<ICardRepository>(() => this.repositoryFactory.CreateRepository<CardRepository>(this, this.cardKeyGenerator, this.nameIdentifier));
            this.cardDecks = new Lazy<ICardDeckRepository>(() => this.repositoryFactory.CreateRepository<CardDeckRepository>(this, this.cardDeckKeyGenerator, this.nameIdentifier));
            this.quizResults = new Lazy<IQuizResultsRepository>(() => this.repositoryFactory.CreateRepository<QuizResultsRepository>(this, this.quizResultKeyGenerator, this.nameIdentifier));
            this.userConfigurations = new Lazy<IUserConfigurationRepository>(() => this.repositoryFactory.CreateRepository<UserConfigurationRepository>(this, this.userConfigurationKeyGenerator, this.nameIdentifier));
            this.masterConfigurations = new Lazy<ITableStorageRepository<MasterConfiguration>>(() => new TableStorageRepository<MasterConfiguration>(this));
        }

        public ICardRepository Cards { get { return this.cards.Value; } }
        public ICardDeckRepository CardDecks { get { return this.cardDecks.Value; } }
        public IQuizResultsRepository QuizResults { get { return this.quizResults.Value; } }
        public IUserConfigurationRepository UserConfigurations { get { return this.userConfigurations.Value; } }
        public ITableStorageRepository<MasterConfiguration> MasterConfigurations { get { return this.masterConfigurations.Value; } }

        public void AddObject(TableServiceEntity entity)
        {
            this.AddObject(this.tableName, entity);
        }

        public void UpdateObject(TableServiceEntity entity)
        {
            var alreadyAttached = false;
            Uri uri;

            if (this.TryGetUri(entity, out uri))
            {
                TableServiceEntity existingEntity;
                if (this.TryGetEntity(uri, out existingEntity))
                {
                    alreadyAttached = true;
                }
            }

            if (!alreadyAttached)
            {
                this.Detach(entity);
                this.AttachTo(this.tableName, entity);
            }

            base.UpdateObject(entity);
        }

        public void DeleteObject(TableServiceEntity entity)
        {
            base.DeleteObject(entity);
        }

        public IQueryable<T> CreateQuery<T>()
        {
            return this.CreateQuery<T>(this.tableName);
        }

        public void Commit()
        {
            this.SaveChangesWithRetries();
        }

        public void CommitBatch()
        {
            this.SaveChangesWithRetries(SaveChangesOptions.Batch);
        }

        public bool Detach(TableServiceEntity entity)
        {
            return base.Detach(entity);
        }

        public void UpdateCardAndRelations(Card card)
        {
            var originalCard = this.Cards.Get(card.PartitionKey, card.RowKey);
            this.Detach(originalCard);

            if (originalCard.DeckName != card.DeckName)
            {
                this.CardDecks.RemoveCardFromCardDeck(originalCard);
                this.CardDecks.AddCardToCardDeck(card);
            }

            this.Cards.Update(card);
        }
    }
}