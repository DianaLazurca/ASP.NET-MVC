using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEvaluator.Repositories
{
    public static class DBManager
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