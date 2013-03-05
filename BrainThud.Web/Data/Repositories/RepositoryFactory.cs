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

        public TReturn CreateRepository<TRepository, TReturn>(ITableStorageContext tableStorageContext, string rowType, string nameIdentifier)
            where TRepository : TableStorageRepositoryBase
        {
//            var concrete = typeof(T).Assembly.GetTypes().First(x => !x.IsInterface && !x.IsAbstract && typeof(T).IsAssignableFrom(x));
            var cardEntityKeyGenerator = new CardEntityKeyGenerator(this.authenticationHelper, this.identityQueueManager, rowType);
            return (TReturn)Activator.CreateInstance(typeof(TRepository), tableStorageContext, cardEntityKeyGenerator, nameIdentifier);
        }
    }
}