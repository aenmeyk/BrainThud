using System;
using System.ComponentModel.DataAnnotations;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Model
{
    public class Card : AzureTableEntity
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        [Range(typeof(DateTime), TypeValues.MIN_SQL_DATETIME_STRING, TypeValues.MAX_SQL_DATETIME_STRING)]
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