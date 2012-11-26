using System;
using System.Collections.Generic;
using BrainThud.Model;

namespace BrainThud.Data.AzureTableStorage
{
    public class EntitySetDictionary : Dictionary<Type, string>
    {
        public EntitySetDictionary()
        {
            this.Add(typeof(Card), EntitySetNames.CARD);
            this.Add(typeof(QuizResult), EntitySetNames.CARD);
            this.Add(typeof(User), EntitySetNames.USER);
        }
    }
}