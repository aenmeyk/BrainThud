using System;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class KeyGeneratorFactory: IKeyGeneratorFactory
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ITableStorageContext tableStorageContext;

        public KeyGeneratorFactory(IAuthenticationHelper authenticationHelper, ITableStorageContext tableStorageContext)
        {
            this.authenticationHelper = authenticationHelper;
            this.tableStorageContext = tableStorageContext;
        }

        public ITableStorageKeyGenerator GetTableStorageKeyGenerator<T>() where T : TableServiceEntity
        {
            if(typeof(T) == typeof(Card))
            {
                return new CardKeyGenerator(this.authenticationHelper, this.tableStorageContext);
            }

            throw new NotSupportedException(string.Format("No key generator exists for type {0}", typeof(T).AssemblyQualifiedName));
        }
    }
}