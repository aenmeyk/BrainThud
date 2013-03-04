using System;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Data.KeyGenerators;

namespace BrainThud.Web.Data.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public T CreateRepository<T>(ITableStorageContext tableStorageContext, ICardEntityKeyGenerator cardEntityKeyGenerator, string nameIdentifier)
            where T : TableStorageRepositoryBase 
        {
            return (T)Activator.CreateInstance(typeof(T), tableStorageContext, cardEntityKeyGenerator, nameIdentifier);
        }
    }
}