using System;
using System.Linq;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public class TableStorageContext<T> :TableServiceContext, ITableStorageContext<T> where T: TableServiceEntity
    {
        private readonly string entitySetName;

        public TableStorageContext(string entitySetName, ICloudStorageServices cloudStorageServices)
            : base(cloudStorageServices.CloudStorageAccount.TableEndpoint.ToString(), cloudStorageServices.CloudStorageAccount.Credentials)
        {
            cloudStorageServices.CreateTableIfNotExist(entitySetName);
            this.entitySetName = entitySetName;
        }

        public void AddObject(T entity)
        {
            this.AddObject(this.entitySetName, entity);
        }

        public void UpdateObject(T entity)
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

        public void DeleteObject(T entity)
        {
            base.DeleteObject(entity);
        }

        public IQueryable<T> CreateQuery(string entitySetName)
        {
            return this.CreateQuery<T>(entitySetName);
        }
    }
}