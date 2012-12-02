using System;
using BrainThud.Web.Data.AzureTableStorage;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class KeyGeneratorFactory: IKeyGeneratorFactory
    {
        private readonly ITableStorageRepository<Configuration> configurationRepository;
        private readonly IAuthenticationHelper authenticationHelper;

        public KeyGeneratorFactory(ITableStorageRepository<Configuration> configurationRepository, IAuthenticationHelper authenticationHelper)
        {
            this.configurationRepository = configurationRepository;
            this.authenticationHelper = authenticationHelper;
        }

        public ITableStorageKeyGenerator GetTableStorageKeyGenerator<T>() where T : TableServiceEntity
        {
            if(typeof(T) == typeof(Card))
            {
                return new CardKeyGenerator(configurationRepository, authenticationHelper);
            }

            throw new NotSupportedException(string.Format("No key generator exists for type {0}", typeof(T).AssemblyQualifiedName));
        }
    }
}