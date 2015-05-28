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
                return context.Evaluations.Include(ev => ev.Test.Questions).Where(x => x.Id == id).SingleOrDefault();
            }
        }
    }
}