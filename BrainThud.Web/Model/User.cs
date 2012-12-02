using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Model
{
    public class User : TableServiceEntity 
    {
        public int Id { get; set; }
        public int LastUsedCardId { get; set; }
    }
}