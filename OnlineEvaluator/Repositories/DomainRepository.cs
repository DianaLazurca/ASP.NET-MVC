using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEvaluator.Repositories
{
    public static class DomainRepository
    {
        public static string AddNewDomain(String domainName)
        {
            if (domainName != null)
            {
                Domain domain = new Domain
                {
                    Name = domainName
                };
                using (var context = new ApplicationDbContext())
                {
                    context.Domains.Add(domain);
                    context.SaveChanges();
                }
                return "success" + domain.Id;
            }
            else
            {
                return "error";
            }

        }

        // sterge si subdomeniile + intrebarile asociate
        public static string RemoveDomainById(int id)
        {
            string result = "does not exist";

            using (var context = new ApplicationDbContext())
            {
                var domain = context.Domains.FirstOrDefault(x => x.Id == id);
                if (domain != null)
                {
                    context.Domains.Remove(domain);
                    context.SaveChanges();
                    result = "deleted";
                }
            }

            return result;
        }

        public static List<Domain> GetAllDomains()
        {
            List<Domain> allDomains = new List<Domain>();
            using (var context = new ApplicationDbContext())
            {
                allDomains = context.Domains.ToList();
            }
            return allDomains;
        }
    }
}