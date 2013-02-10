using BrainThud.Core.Data.AzureTableStorage;

namespace BrainThud.Core.Models
{
    public class MasterConfiguration : AzureTableEntity
    {
        public int CurrentMaxIdentity { get; set; }
    }
}