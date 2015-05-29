using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineEvaluator.Repositories
{
    public class EvaluationRepository
    {

        public static Evaluation AddEvaluation(Evaluation evaluation)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Evaluations.Add(evaluation);
                context.SaveChanges();
                return evaluation;
            }
        }

        public static Evaluation GetEvaluationById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Evaluations.Include(ev => ev.Test.Questions).Include(ev => ev.Test.Domain)
                    .Include(ev => ev.Test.Questions.Select(q => q.Answers))
                    .Include(ev => ev.Test.Questions.Select(q => q.Subdomain))
                    .Include(ev => ev.EvaluationAnswers)
                    .Include(ev => ev.EvaluationAnswers.Select(ea => ea.Answer))
                    .Include(ev => ev.EvaluationJustifications)
                    .Where(x => x.Id == id).SingleOrDefault();
            }
        }

        internal static Evaluation UpdateEvaluation(int id, Evaluation evaluation)
        {
            using (var context = new ApplicationDbContext())
            {
                Evaluation oldEvaluation = context.Evaluations.Where(ev => ev.Id == id).FirstOrDefault();

                if (oldEvaluation != null)
                {
                    oldEvaluation.EvaluationAnswers = evaluation.EvaluationAnswers;
                    oldEvaluation.EvaluationJustifications = evaluation.EvaluationJustifications;

                    context.SaveChanges();

                    return oldEvaluation;
                }

                return null;
            }
        }
    }
}