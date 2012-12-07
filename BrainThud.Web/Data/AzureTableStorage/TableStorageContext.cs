using System;
using System.Data.Services.Client;
using System.Linq;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContext : TableServiceContext, ITableStorageContext
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly string entitySetName;
        private readonly Lazy<ITableStorageRepository<Card>> cards;
        private readonly Lazy<ITableStorageRepository<QuizResult>> quizResults;
        private readonly Lazy<ITableStorageRepository<Configuration>> configurations;
        private readonly Lazy<ITableStorageRepository<MasterConfiguration>> masterConfigurations;

        public TableStorageContext(
            ICloudStorageServices cloudStorageServices, 
            IAuthenticationHelper authenticationHelper,
            string entitySetName)
            : base(cloudStorageServices.CloudStorageAccount.TableEndpoint.ToString(), cloudStorageServices.CloudStorageAccount.Credentials)
        {
            this.authenticationHelper = authenticationHelper;
            this.entitySetName = entitySetName;
            cloudStorageServices.CreateTableIfNotExists(entitySetName);
            this.cards = this.InitializeLazyRepository<Card>();
            this.quizResults = this.InitializeLazyRepository<QuizResult>();
            this.configurations = this.InitializeLazyRepository<Configuration>();
            this.masterConfigurations = this.InitializeLazyRepository<MasterConfiguration>();
            this.IgnoreResourceNotFoundException = true;
        }

        private Lazy<ITableStorageRepository<T>> InitializeLazyRepository<T>() where T: TableServiceEntity
        {
            return new Lazy<ITableStorageRepository<T>>(() => new TableStorageRepository<T>(this, this.authenticationHelper));
        }

        public ITableStorageRepository<Card> Cards { get { return this.cards.Value; } }
        public ITableStorageRepository<QuizResult> QuizResults { get { return this.quizResults.Value; } }
        public ITableStorageRepository<Configuration> Configurations { get { return this.configurations.Value; } }
        public ITableStorageRepository<MasterConfiguration> MasterConfigurations { get { return this.masterConfigurations.Value; } }

        public void AddObject(TableServiceEntity entity)
        {
            this.AddObject(this.entitySetName, entity);
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
                this.AttachTo(this.entitySetName, entity);
            }

            base.UpdateObject(entity);
        }

        public void DeleteObject(TableServiceEntity entity)
        {
            base.DeleteObject(entity);
        }

        public IQueryable<T> CreateQuery<T>()
        {
            return this.CreateQuery<T>(this.entitySetName);
        }

        public void Commit()
        {
            this.SaveChangesWithRetries();
        }
    }
}