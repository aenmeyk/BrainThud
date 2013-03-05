using System;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureQueues;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly IIdentityQueueManager identityQueueManager;

        public RepositoryFactory(IAuthenticationHelper authenticationHelper, IIdentityQueueManager identityQueueManager)
        {
            this.authenticationHelper = authenticationHelper;
            this.identityQueueManager = identityQueueManager;
        }

        public T CreateRepository<T>(ITableStorageContext tableStorageContext, string rowType, string nameIdentifier)
            where T : TableStorageRepositoryBase
        {
            var cardEntityKeyGenerator = new CardEntityKeyGenerator(this.authenticationHelper, this.identityQueueManager, rowType);
            return (T)Activator.CreateInstance(typeof(T), tableStorageContext, cardEntityKeyGenerator, nameIdentifier);
        }
    }
}