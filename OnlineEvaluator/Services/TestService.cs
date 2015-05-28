using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEvaluator.Services
{
    public class TestService
    {

        public Test GenerateTest(List<int> subdomainsIds)
        {
            List<Question> questionsWithMultipleAnswers = new List<Question>();
            List<Question> questionsWithSingleAnswer = new List<Question>();

            if (subdomainsIds.Count() < 10)
            {

            }
            else
            {

            }


            return null;
        }
    }
}