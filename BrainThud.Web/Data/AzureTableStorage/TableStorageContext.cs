using System;
using System.Linq;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TableStorageContext :TableServiceContext, ITableStorageContext
    {
        private readonly string entitySetName;

        public TableStorageContext(ICloudStorageServices cloudStorageServices, string entitySetName)
            : base(cloudStorageServices.CloudStorageAccount.TableEndpoint.ToString(), cloudStorageServices.CloudStorageAccount.Credentials)
        {
            this.entitySetName = entitySetName;
            cloudStorageServices.CreateTableIfNotExist(entitySetName);
        }

        public void AddObject(TableServiceEntity entity)
        {
            this.AddObject(this.entitySetName, entity);
        }

        public void UpdateObject(TableServiceEntity entity)
        {
            var alreadyAttached = false;
            Uri uri;

            if(this.TryGetUri(entity, out uri))
            {
                TableServiceEntity existingEntity;
                if(this.TryGetEntity(uri, out existingEntity))
                {
                    alreadyAttached = true;
                }
            }

            if(!alreadyAttached) this.AttachTo(this.entitySetName, entity);
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
    }
}