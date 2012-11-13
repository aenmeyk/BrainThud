using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Model
{
    public class Card : TableServiceEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}