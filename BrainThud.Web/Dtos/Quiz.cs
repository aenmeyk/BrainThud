﻿using System;
using System.Collections.Generic;
using BrainThud.Web.Model;

namespace BrainThud.Web.Dtos
{
    public class Quiz
    {
        public IEnumerable<Card> Cards { get; set; }
        public string ResultsUri { get; set; }
        public int UserId { get; set; }
        public DateTime QuizDate { get; set; }
        public IEnumerable<QuizResult> QuizResults { get; set; }
    }
}