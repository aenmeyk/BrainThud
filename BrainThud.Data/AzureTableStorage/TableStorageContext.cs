using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public class TableStorageContext<T> :TableServiceContext, ITableStorageContext<T> where T: TableServiceEntity
    {
        private readonly string entitySetName;


        public TableStorageContext(string entitySetName, CloudStorageAccount cloudStorageAccount, bool createTable = true)
            : base(cloudStorageAccount.TableEndpoint.ToString(), cloudStorageAccount.Credentials)
        {
            if (createTable) cloudStorageAccount.CreateCloudTableClient().CreateTableIfNotExist(entitySetName);
            this.entitySetName = entitySetName;
        }

        public void AddObject(T entity)
        {
            this.AddObject(this.entitySetName, entity);
        }
    }
}