using System;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Model
{
    public class QuizResult : TableServiceEntity
    {
        public DateTime QuizDate { get; set; }
        public int CardId { get; set; }
        public bool IsCorrect { get; set; }
        public int UserId { get; set; }
        public int EntityId { get; set; }
    }
}