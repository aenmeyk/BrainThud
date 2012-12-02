using System.Data.Services.Client;
using System.Linq;
using BrainThud.Web.Model;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ITableStorageContext
    {
        void AddObject(TableServiceEntity entity);
        void UpdateObject(TableServiceEntity entity);
        void DeleteObject(TableServiceEntity entity);
        DataServiceResponse SaveChangesWithRetries();
        IQueryable<T> CreateQuery<T>();
        ITableStorageRepository<Card> Cards { get; }
        ITableStorageRepository<QuizResult> QuizResults { get; }
        void Commit();
    }
}