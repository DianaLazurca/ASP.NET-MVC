using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEvaluator.Services
{
    public class EvaluationService
    {
        public EvaluationResult Evaluate(Evaluation evaluation)
        {
            Dictionary<int, bool> questionsState = new Dictionary<int, bool>();
            foreach (Question question in evaluation.Test.Questions)
            {
                questionsState.Add(question.Id, true);
            }
            int score = 0;
            foreach (EvaluationAnswer givenAnswer in evaluation.EvaluationAnswers)
            {
                if (givenAnswer.GivenAnswer != givenAnswer.Answer.IsCorect)
                {
                    questionsState[givenAnswer.QuestionId] = false;
                    score--;
                }
                else
                {
                    score++;
                }
            }
            int correctAnswersCount = 0;
            int incorrectAnswersCount = 0;

            foreach (bool questionState in questionsState.Values)
            {
                if (questionState == true)
                {
                    correctAnswersCount++;
                }
                else
                {
                    incorrectAnswersCount++;
                }
            }

            return new EvaluationResult
            {
                CorrectlyAnsweredQuestionsCount = correctAnswersCount,
                IncorrectlyAnsweredQuestionsCount = incorrectAnswersCount,
                TotalQuestionsCount = questionsState.Count,
                Score = score
            };
        }
    }
}