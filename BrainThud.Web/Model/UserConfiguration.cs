using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Model
{
    public class UserConfiguration : AzureTableEntity 
    {
        public int UserId { get; set; }
    }
}