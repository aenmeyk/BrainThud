using System;
using System.Data.Services.Client;
using System.Linq;
using BrainThud.Web.Data.KeyGenerators;
using BrainThud.Web.Data.Repositories;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContext : TableServiceContext, ITableStorageContext
    {
        private readonly ICardEntityKeyGenerator cardKeyGenerator;
        private readonly ICardEntityKeyGenerator quizResultKeyGenerator;
        private readonly IUserHelper userHelper;
        private readonly string tableName;
        private readonly string nameIdentifier;
        private readonly Lazy<ICardEntityRepository<Card>> cards;
        private readonly Lazy<ICardEntityRepository<QuizResult>> quizResults;
        private readonly Lazy<IUserConfigurationRepository> userConfigurations;
        private readonly Lazy<ITableStorageRepository<MasterConfiguration>> masterConfigurations;

        public TableStorageContext(
            ICloudStorageServices cloudStorageServices, 
            ICardEntityKeyGenerator cardKeyGenerator,
            ICardEntityKeyGenerator quizResultKeyGenerator,
            IUserHelper userHelper,
            string tableName, 
            string nameIdentifier)
            : base(cloudStorageServices.CloudStorageAccount.TableEndpoint.ToString(), cloudStorageServices.CloudStorageAccount.Credentials)
        {
            this.cardKeyGenerator = cardKeyGenerator;
            this.quizResultKeyGenerator = quizResultKeyGenerator;
            this.userHelper = userHelper;
            this.tableName = tableName;
            this.nameIdentifier = nameIdentifier;
            this.IgnoreResourceNotFoundException = true;
            this.cards = new Lazy<ICardEntityRepository<Card>>(() => new CardRepository(this, this.cardKeyGenerator, this.nameIdentifier));
            this.quizResults = new Lazy<ICardEntityRepository<QuizResult>>(() => new QuizResultsRepository(this, this.quizResultKeyGenerator, this.nameIdentifier));
            this.userConfigurations = new Lazy<IUserConfigurationRepository>(() => new UserConfigurationRepository(this, this.cardKeyGenerator, this.userHelper, this.nameIdentifier));
            this.masterConfigurations = new Lazy<ITableStorageRepository<MasterConfiguration>>(() => new TableStorageRepository<MasterConfiguration>(this));
        }

        public ICardEntityRepository<Card> Cards { get { return this.cards.Value; } }
        public ICardEntityRepository<QuizResult> QuizResults { get { return this.quizResults.Value; } }
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

            if(!alreadyAttached)
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
    }
}