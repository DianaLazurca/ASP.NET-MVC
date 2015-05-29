using OnlineEvaluator.Models;
using OnlineEvaluator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEvaluator.Services
{
    public class TestService
    {
        public ICollection<Question> GenerateRandomQuestions(List<int> selectedSubdomains)
        {
            return QuestionRepository.GetQuestionsBySudomainIdList(selectedSubdomains);
        }

        public List<Question> GenerateTestListOfQuestions(List<int> subdomainsIds)
        {
            List<Question> finalListWithQuestions = new List<Question>();

            List<Question> questionsWithMultipleAnswers = new List<Question>();
            List<Question> questionsWithSingleAnswer = new List<Question>();
            int HowManyQuestionsForEachSubdomain;
            Random randomGenerator = new Random();
            int nrOfQuestionsWithSingleAnswer ;

            if (subdomainsIds.Count() < 10)
            {
              
                if (subdomainsIds.Count() == 1)
                {
                    
                    List<Question> allSingleAnswerQuestionsForSUbdomaID = SubdomainRepository.GetAllQuestionsOfTypeForSubdomainId(subdomainsIds.ElementAt(0), false);
                    List<Question> allMultipleAnswerQuestionsForSUbdomainID = SubdomainRepository.GetAllQuestionsOfTypeForSubdomainId(subdomainsIds.ElementAt(0), true);

                    finalListWithQuestions = GenerateFInalTestListOfQuestions(allSingleAnswerQuestionsForSUbdomaID, allMultipleAnswerQuestionsForSUbdomainID);

                }
                else
                {
                    HowManyQuestionsForEachSubdomain = (int)Math.Round(10.0 / subdomainsIds.Count());

                    List<Question> allSingles = new List<Question>();
                    List<Question> allMultiples = new List<Question>();

                    for (int i = 0; i < subdomainsIds.Count(); i++)
                    {
                        List<Question> allSingleAnswerQuestionsForSUbdomaID = SubdomainRepository.GetAllQuestionsOfTypeForSubdomainId(subdomainsIds.ElementAt(i), false);
                        List<Question> allMultipleAnswerQuestionsForSUbdomainID = SubdomainRepository.GetAllQuestionsOfTypeForSubdomainId(subdomainsIds.ElementAt(i), true);

                        nrOfQuestionsWithSingleAnswer = randomGenerator.Next(2, HowManyQuestionsForEachSubdomain);
                        int oldValue = allSingles.Count();
                        while (allSingles.Count() != oldValue + nrOfQuestionsWithSingleAnswer)
                        {
                            int randomIndexOfQuestion = randomGenerator.Next(0, allSingleAnswerQuestionsForSUbdomaID.Count());

                            if (!allSingles.Contains(allSingleAnswerQuestionsForSUbdomaID.ElementAt(randomIndexOfQuestion))) 
                            {

                                allSingles.Add(allSingleAnswerQuestionsForSUbdomaID.ElementAt(randomIndexOfQuestion));
                            }
                        }
                        oldValue = allMultiples.Count();
                        int  nrOfQuestionsWithMultipleAnswer = Math.Max(randomGenerator.Next(3, HowManyQuestionsForEachSubdomain + 1), HowManyQuestionsForEachSubdomain - nrOfQuestionsWithSingleAnswer);
                        while (allMultiples.Count != oldValue + nrOfQuestionsWithMultipleAnswer)
                        {
                            int randomIndexOfQuestion = randomGenerator.Next(0, allMultipleAnswerQuestionsForSUbdomainID.Count());

                            if (!allMultiples.Contains(allMultipleAnswerQuestionsForSUbdomainID.ElementAt(randomIndexOfQuestion)))
                            {

                                allMultiples.Add(allMultipleAnswerQuestionsForSUbdomainID.ElementAt(randomIndexOfQuestion));
                            }
                        }

                    }
                    
                    finalListWithQuestions = GenerateFInalTestListOfQuestions(allSingles, allMultiples);
                }
                
            }
            else
            {
                if (subdomainsIds.Count() == 10)
                {
                    finalListWithQuestions = GenerateFinalListOFQuestionstForMoreThan10Subdomains(subdomainsIds);
                } else {

                    List<int> pick10SubdomainsIdx = new List<int>();

                    while (pick10SubdomainsIdx.Count() != 10)
                    {
                        int subdomaiIdx = randomGenerator.Next(0, subdomainsIds.Count());
                        if (!pick10SubdomainsIdx.Contains(subdomaiIdx))
                        {
                            pick10SubdomainsIdx.Add(subdomainsIds.ElementAt(subdomaiIdx));
                        }
                    }

                    finalListWithQuestions = GenerateFinalListOFQuestionstForMoreThan10Subdomains(pick10SubdomainsIdx);
                }

            }


            return finalListWithQuestions;
        }

        private List<Question> GenerateFinalListOFQuestionstForMoreThan10Subdomains(List<int> allSubdomains)
        {
            Random randomGenerator = new Random();
            List<Question> allQuestions = new List<Question>();
            int nrOfQuestionsWithSingleAnswer = randomGenerator.Next(3, 6);
            List<int> allSubdomainsWithSingleQuestions = new List<int>();

            while (allSubdomainsWithSingleQuestions.Count() != nrOfQuestionsWithSingleAnswer)
            {
                int subdomainIndex = randomGenerator.Next(0, allSubdomains.Count());

                if (!allSubdomainsWithSingleQuestions.Contains(subdomainIndex))
                {
                    allSubdomainsWithSingleQuestions.Add(subdomainIndex);
                }
            }
           
            for (int i = 0; i < allSubdomains.Count(); i++)
            {
                List<Question> questionsList = new List<Question>();
                if (allSubdomainsWithSingleQuestions.Contains(i))
                {
                    questionsList = SubdomainRepository.GetAllQuestionsOfTypeForSubdomainId(allSubdomains.ElementAt(i), false);
                    
                }
                else
                {
                    questionsList = SubdomainRepository.GetAllQuestionsOfTypeForSubdomainId(allSubdomains.ElementAt(i), true);
                }

                int randomQuestionId = randomGenerator.Next(0, questionsList.Count());
                allQuestions.Add(questionsList.ElementAt(i));
            }

            return allQuestions;
        }
        private List<Question> GenerateFInalTestListOfQuestions(List<Question> allSingleAnswerQuestionsForSUbdomaID, List<Question> allMultipleAnswerQuestionsForSUbdomainID)
        {
            List<Question> questionsWithMultipleAnswers = new List<Question>();
            Random randomGenerator = new Random();
            List<Question> questionsWithSingleAnswer = new List<Question>();
            int nrOfQuestionsWithSingleAnswer = randomGenerator.Next(3, 6);

            while (questionsWithSingleAnswer.Count() != nrOfQuestionsWithSingleAnswer)
            {
                int randomIndexForQuestion = randomGenerator.Next(0, allSingleAnswerQuestionsForSUbdomaID.Count());

                if (!questionsWithSingleAnswer.Contains(allSingleAnswerQuestionsForSUbdomaID.ElementAt(randomIndexForQuestion)))
                {
                    questionsWithSingleAnswer.Add(allSingleAnswerQuestionsForSUbdomaID.ElementAt(randomIndexForQuestion));
                }
            }


            while (questionsWithMultipleAnswers.Count() != (10 - nrOfQuestionsWithSingleAnswer))
            {
                int randomIndexForQuestion = randomGenerator.Next(0, allMultipleAnswerQuestionsForSUbdomainID.Count());

                if (!questionsWithMultipleAnswers.Contains(allMultipleAnswerQuestionsForSUbdomainID.ElementAt(randomIndexForQuestion)))
                {
                    questionsWithMultipleAnswers.Add(allMultipleAnswerQuestionsForSUbdomainID.ElementAt(randomIndexForQuestion));
                }
            }

            return questionsWithSingleAnswer.Concat<Question>(questionsWithMultipleAnswers).ToList();
        }
    }
}