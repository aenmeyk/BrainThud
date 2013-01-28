using System;
using BrainThud.Core.Data.AzureTableStorage;

namespace BrainThud.Core.Models
{
    public class Card : AzureTableEntity
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public DateTime QuizDate { get; set; }

        public int Level { get; set; }

        public string DeckName { get; set; }

        public string DeckNameSlug { get; set; }

        public string Tags { get; set; }

        public int EntityId { get; set; }

        public int UserId { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime CompletedQuizDate { get; set; }
    }
}