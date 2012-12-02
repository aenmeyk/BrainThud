using System;
using BrainThud.Web.Helpers;
using BrainThud.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.KeyGenerators
{
    public class KeyGeneratorFactory: IKeyGeneratorFactory
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public KeyGeneratorFactory(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        public ITableStorageKeyGenerator GetTableStorageKeyGenerator<T>() where T : TableServiceEntity
        {
            if(typeof(T) == typeof(Card))
            {
                return new CardKeyGenerator(authenticationHelper);
            }

            throw new NotSupportedException(string.Format("No key generator exists for type {0}", typeof(T).AssemblyQualifiedName));
        }
    }
}