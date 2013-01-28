using System;
using BrainThud.Core.Data.AzureTableStorage;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Model
{
    public class QuizResult : AzureTableEntity
    {
        public DateTime QuizDate { get; set; }
        public int QuizYear { get; set; }
        public int QuizMonth { get; set; }
        public int QuizDay { get; set; }
        public int CardId { get; set; }
        public bool IsCorrect { get; set; }
        public int UserId { get; set; }
        public int EntityId { get; set; }

        /// <summary>
        /// Indicates the value of the card's Level property before this QuizResult was applied
        /// </summary>
        public int CardLevel { get; set; }

        /// <summary>
        /// Indicates the value of the card's QuizDate property before this QuizResult was applied
        /// </summary>
        public DateTime CardQuizDate { get; set; }


        /// <summary>
        /// Indicates the value of the card's IsCorrect property before this QuizResult was applied
        /// </summary>
        public bool CardIsCorrect { get; set; }


        /// <summary>
        /// Indicates the value of the card's CompletedQuizDate property before this QuizResult was applied
        /// </summary>
        public DateTime CardCompletedQuizDate { get; set; }

    }
}