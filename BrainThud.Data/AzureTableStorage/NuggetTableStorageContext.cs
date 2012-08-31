using BrainThud.Model;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Data.AzureTableStorage
{
    public class NuggetTableStorageContext :TableServiceContext, ITableStorageContext<Nugget>
    {
        private const string ENTITY_SET_NAME = "Nugget";

        public NuggetTableStorageContext(string baseAddress, StorageCredentials credentials) 
            : base(baseAddress, credentials) {}

        public void AddObject(Nugget entity)
        {
            this.AddObject(ENTITY_SET_NAME, entity);
        }
    }
}