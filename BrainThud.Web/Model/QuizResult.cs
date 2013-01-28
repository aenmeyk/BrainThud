using System;
using BrainThud.Core.Data.AzureTableStorage;

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

        // The following fields indicate the value of the card's properties before this QuizResult was applied.
        // We record them in the QuizResult so if the QuizResult is reversed, the values can be reset on the card.
        public int CardLevel { get; set; }
        public DateTime CardQuizDate { get; set; }
        public bool CardIsCorrect { get; set; }
        public int CardCompletedQuizYear { get; set; }
        public int CardCompletedQuizMonth { get; set; }
        public int CardCompletedQuizDay { get; set; }
    }
}