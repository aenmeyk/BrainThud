using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Model
{
    public class MasterConfiguration : TableServiceEntity
    {
        public int CurrentMaxIdentity { get; set; }
    }
}