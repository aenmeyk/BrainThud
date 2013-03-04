using System;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Data.Repositories
{
    public class TableStorageRepositoryBase
    {
        private readonly Lazy<int> userId;

        public TableStorageRepositoryBase(ITableStorageContext tableStorageContext)
        {
            this.TableStorageContext = tableStorageContext;
            this.userId = new Lazy<int>(() => this.TableStorageContext.UserConfigurations.GetByNameIdentifier().UserId);
        }

        protected ITableStorageContext TableStorageContext { get; private set; }
        protected int UserId { get { return this.userId.Value; } }
    }
}