using System;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class AzureTableEntity : TableServiceEntity
    {
        public DateTime CreatedTimestamp { get; set; }
    }
}