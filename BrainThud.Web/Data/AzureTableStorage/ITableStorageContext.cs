using System.Data.Services.Client;
using System.Linq;
using BrainThud.Core.Models;
using BrainThud.Web.Data.Repositories;
using Microsoft.WindowsAzure.Storage.Table.DataServices;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public interface ITableStorageContext
    {
        void AddObject(TableServiceEntity entity);
        void UpdateObject(TableServiceEntity entity);
        void DeleteObject(TableServiceEntity entity);
        DataServiceResponse SaveChangesWithRetries();
        IQueryable<T> CreateQuery<T>();
        ICardRepository Cards { get; }
        ICardDeckRepository CardDecks { get; }
        IQuizResultsRepository QuizResults { get; }
        IUserConfigurationRepository UserConfigurations { get; }
        ITableStorageRepository<MasterConfiguration> MasterConfigurations { get; }
        void Commit();
        bool Detach(TableServiceEntity entity);
        void CommitBatch();
        void UpdateCardAndRelations(Card card);
    }
}