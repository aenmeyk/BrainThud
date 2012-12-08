﻿using BrainThud.Web.Model;

namespace BrainThud.Web.Data.Repositories
{
    public interface IUserRepository : ICardEntityRepository<UserConfiguration>
    {
        UserConfiguration GetByNameIdentifier();
    }
}