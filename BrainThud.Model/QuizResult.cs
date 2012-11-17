using System;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Model
{
    public class QuizResult : TableServiceEntity
    {
        public DateTime QuizDate { get; set; }
    }
}