using System;
using System.Linq;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class TestTableStorageContext :TableServiceContext //, ITableStorageContext<T> where T: TableServiceEntity
    {
        private readonly string entitySetName;

        public TestTableStorageContext(ICloudStorageServices cloudStorageServices, string entitySetName)
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



//namespace BrainThud.Data.AzureTableStorage
//{
//    public class OldTableStorageContext<T> :TableServiceContext, ITableStorageContext<T> where T: TableServiceEntity
//    {
//        private readonly string entitySetName;
//
//        public OldTableStorageContext(ICloudStorageServices cloudStorageServices)
//            : base(cloudStorageServices.CloudStorageAccount.TableEndpoint.ToString(), cloudStorageServices.CloudStorageAccount.Credentials)
//        {
//            this.entitySetName = new EntitySetDictionary()[typeof(T)];
//            cloudStorageServices.CreateTableIfNotExist(entitySetName);
//        }
//
//        public void AddObject(T entity)
//        {
//            this.AddObject(this.entitySetName, entity);
//        }
//
//        public void UpdateObject(T entity)
//        {
//            var alreadyAttached = false;
//            Uri uri;
//
//            if(this.TryGetUri(entity, out uri))
//            {
//                TableServiceEntity existingEntity;
//                if(this.TryGetEntity(uri, out existingEntity))
//                {
//                    alreadyAttached = true;
//                }
//            }
//
//            if(!alreadyAttached) this.AttachTo(this.entitySetName, entity);
//            base.UpdateObject(entity);
//        }
//
//        public void DeleteObject(T entity)
//        {
//            base.DeleteObject(entity);
//        }
//
//        public IQueryable<T> CreateQuery()
//        {
//            return this.CreateQuery<T>(this.entitySetName);
//        }
//    }
//}

//
//
//namespace BrainThud.Data.AzureTableStorage
//{
//using System.Collections.Generic;
//using System.Data.Services.Client;
//using System.Linq;
//using BrainThud.Data.KeyGenerators;
//using BrainThud.Model;
//using Microsoft.WindowsAzure.StorageClient;
//    public class OldTableStorageRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
//    {
//        private readonly ITableStorageContext<T> tableStorageContext;
//        private readonly ITableStorageKeyGenerator keyGenerator;
//
//        public OldTableStorageRepository(ITableStorageContext<T> tableStorageContext, ITableStorageKeyGenerator keyGenerator)
//        {
//            this.tableStorageContext = tableStorageContext;
//            this.keyGenerator = keyGenerator;
//        }
//
//        private IQueryable<T> entitySet
//        {
//            get
//            {
//                var queryable = this.tableStorageContext.CreateQuery();
//#if DEBUG
//                if(typeof(ITestData).IsAssignableFrom(typeof(T)))
//                {
//                    return queryable.Where(x => ((ITestData)x).IsTestData);
//                }
//#endif
//                return queryable;
//            }
//        }
//
//        public void Add(T entity)
//        {
//            if (string.IsNullOrEmpty(entity.PartitionKey)) entity.PartitionKey = this.keyGenerator.GeneratePartitionKey();
//            if (string.IsNullOrEmpty(entity.RowKey)) entity.RowKey = this.keyGenerator.GenerateRowKey();
//
//#if DEBUG
//            var mockable = entity as ITestData;
//            if(mockable != null) mockable.IsTestData = true;
//#endif
//
//            this.tableStorageContext.AddObject(entity);
//        }
//
//        public void Update(T entity)
//        {
//            this.tableStorageContext.UpdateObject(entity);
//        }
//
//        public void Delete(string rowKey)
//        {
//            var item = this.Get(rowKey);
//            this.tableStorageContext.DeleteObject(item);
//        }
//
//        public T Get(string rowKey)
//        {
//            return this.entitySet.Where(x => x.PartitionKey == Keys.TEMP_PARTITION_KEY && x.RowKey == rowKey).First();
//        }
//
//        public IQueryable<T> GetAll()
//        {
//            return this.entitySet;
//        }
//
//        public void Commit()
//        {
//            this.tableStorageContext.SaveChangesWithRetries();
////            ((TableStorageContext<T>)this.tableStorageContext).SaveChangesWithRetries( SaveChangesOptions.Batch);
//        }
//    }
//}