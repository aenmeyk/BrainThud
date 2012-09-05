using System.Collections.Generic;
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
            this.AttachTo(this.entitySetName, entity);
            base.UpdateObject(entity);
        }

        public IEnumerable<T> CreateQuery(string entitySetName)
        {
            return this.CreateQuery<T>(entitySetName);
        }
    }
}