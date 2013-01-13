using System;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Model
{
    public class QuizResult : AzureTableEntity
    {
        public DateTime QuizDate { get; set; }
        public int CardId { get; set; }
        public bool IsCorrect { get; set; }
        public int UserId { get; set; }
        public int EntityId { get; set; }
    }
}