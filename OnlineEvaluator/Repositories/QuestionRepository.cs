using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineEvaluator.Repositories
{
    public class QuestionRepository
    {
        public static bool RemoveQUestionById(int id)
        {
            using (var context = new ApplicationDbContext())
            {

                var question = context.Questions.FirstOrDefault(x => x.Id == id);
                if (question != null)
                {
                    context.Questions.Remove(question);
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public static Question AddNewQuestion(Question question)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Subdomains.Any(sd => sd.Id == question.SubdomainId))
                {
                    context.Questions.Add(question);
                    context.SaveChanges();

                    return question;
                }

                return null;
            }
        }

        public static bool EditQuestion(int questionId, Question editedQuestion) 
        {
            using(var context = new ApplicationDbContext())
            {
                Question question = context.Questions.FirstOrDefault(q => q.Id == questionId);
                if (question != null)
                {
                    context.Answers.Where(a => a.QuestionId == questionId).ToList().ForEach(oa => context.Answers.Remove(oa));

                    question.IsMultiple = editedQuestion.IsMultiple;
                    question.Justification = editedQuestion.Justification;
                    question.Text = editedQuestion.Text;

                    question.Answers = editedQuestion.Answers;

                    context.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public static Question GetQuestionById(int questionId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Questions.Include("Answers").FirstOrDefault(q => q.Id == questionId);
            }
        }
    }
}