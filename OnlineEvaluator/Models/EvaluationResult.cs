using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEvaluator.Models
{
    public class EvaluationResult
    {
        public int CorrectlyAnsweredQuestionsCount { get; set; }

        public int IncorrectlyAnsweredQuestionsCount { get; set; }

        public int TotalQuestionsCount { get; set; }

        public int Score { get; set; }
    }
}