using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineEvaluator.Repositories
{
    public class TestRepository
    {

        public static Test AddTest(Test test)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Tests.Add(test);
                context.SaveChanges();
                return test;
            }
        }

        public static Test GetTestById(int id)
        {
            using (var context = new ApplicationDbContext())
            {

                //Test test = context.Tests.Include("Questions").Include("Answers").Where(x => x.Id == id).SingleOrDefault();
                return context.Tests.Include("Questions").Where(x => x.Id == id).SingleOrDefault();

            }
        }
    }
}