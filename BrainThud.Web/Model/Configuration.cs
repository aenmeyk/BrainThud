using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Model
{
    public class Configuration : TableServiceEntity 
    {
        public int LastUsedId { get; set; }
    }
}