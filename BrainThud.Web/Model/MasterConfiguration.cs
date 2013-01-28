using BrainThud.Core.Data.AzureTableStorage;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Model
{
    public class MasterConfiguration : AzureTableEntity
    {
        public int CurrentMaxIdentity { get; set; }
    }
}