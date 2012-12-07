using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.StorageClient;

namespace BrainThud.Web.Model
{
    public class Card : TableServiceEntity
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        [Range(typeof(DateTime), TypeValues.MIN_SQL_DATETIME, TypeValues.MAX_SQL_DATETIME)]
        public DateTime QuizDate { get; set; }

        public int Level { get; set; }

        public string DeckName { get; set; }

        public string Tags { get; set; }

        public bool IsTestData { get; set; }
    }
}