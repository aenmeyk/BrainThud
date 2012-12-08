using System.Data.Services.Client;
using System.Linq;
using BrainThud.Web.Data.Repositories;
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
        ICardEntityRepository<Card> Cards { get; }
        ICardEntityRepository<QuizResult> QuizResults { get; }
        IUserConfigurationRepository UserConfigurations { get; }
        ITableStorageRepository<MasterConfiguration> MasterConfigurations { get; }
        void Commit();
        bool Detach(TableServiceEntity entity);
        void CommitBatch();
    }
}