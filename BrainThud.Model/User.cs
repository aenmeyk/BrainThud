using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Model
{
    public class User : TableServiceEntity 
    {
        public int Id { get; set; }
        public int LastUsedCardId { get; set; }
    }
}