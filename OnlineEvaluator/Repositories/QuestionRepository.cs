using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}