using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.KeyGenerators
{
    public interface IKeyGeneratorFactory 
    {
        ITableStorageKeyGenerator GetTableStorageKeyGenerator<T>() where T : TableServiceEntity;
    }
}