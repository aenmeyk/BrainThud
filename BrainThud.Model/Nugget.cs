using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Model
{
    public class Nugget : TableServiceEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AdditionalInformation { get; set; }
    }
}