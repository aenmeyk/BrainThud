using System;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Model
{
    public class QuizResult : TableServiceEntity, ITestData
    {
        public DateTime QuizDate { get; set; }
        public string CardId { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsTestData { get; set; }
    }
}