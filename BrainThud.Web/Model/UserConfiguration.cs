using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Model
{
    public class UserConfiguration : TableServiceEntity 
    {
        public int UserId { get; set; }
        public int LastUsedId { get; set; }
    }
}