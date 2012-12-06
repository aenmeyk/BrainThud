using System;
using System.Collections.Generic;
using BrainThud.Web.Model;

namespace BrainThud.Web.Data.AzureTableStorage
{
    public class EntitySetDictionary : Dictionary<Type, string>
    {
        public EntitySetDictionary()
        {
            this.Add(typeof(Card), EntitySetNames.CARD);
            this.Add(typeof(QuizResult), EntitySetNames.CARD);
        }
    }
}