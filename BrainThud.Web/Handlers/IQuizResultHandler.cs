﻿using BrainThud.Web.Model;

namespace BrainThud.Web.Handlers
{
    public interface IQuizResultHandler 
    {
        void UpdateCardLevel(QuizResult quizResult, Card card);
    }
}