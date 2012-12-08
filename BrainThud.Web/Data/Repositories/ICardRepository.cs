﻿using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Data.Repositories
{
    public interface ICardRepository<T> : ITableStorageRepository<T> where T : TableServiceEntity
    {
        T GetById(int userId, int cardId);
        void DeleteById(int userId, int cardId);
    }
}