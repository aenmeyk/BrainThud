using System;
using System.Linq;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public class UserRepository : CardEntityRepository <UserConfiguration>, IUserRepository
    {
        public UserRepository(ITableStorageContext tableStorageContext, string nameIdentifier)
            : base(tableStorageContext, nameIdentifier, CardRowTypes.CONFIGURATION) { }

        public UserConfiguration GetByNameIdentifier()
        {
// ReSharper disable ReplaceWithSingleCallToFirstOrDefault
            return this.EntitySet.Where(x =>
                string.Compare(x.PartitionKey, this.NameIdentifier + '-', StringComparison.Ordinal) >= 0
                && string.Compare(x.PartitionKey, this.NameIdentifier + '.', StringComparison.Ordinal) < 0 
                && x.RowKey == EntityNames.CONFIGURATION)
                .FirstOrDefault();
// ReSharper restore ReplaceWithSingleCallToFirstOrDefault
        }
    }
}