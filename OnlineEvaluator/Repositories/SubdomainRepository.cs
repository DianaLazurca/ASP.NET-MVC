using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineEvaluator.Repositories
{
    public static class SubdomainRepository
    {
        public static Subdomain AddSubdomain(int domainId, string subdomainName)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Domains.Any(d => d.Id == domainId))
                {
                    if (!context.Subdomains.Any(sd => (sd.Name.ToLower() == subdomainName.ToLower()) && (sd.DomainId == domainId)))
                    {
                        Subdomain subdomain = new Subdomain { Name = subdomainName, DomainId = domainId };
                        context.Subdomains.Add(subdomain);
                        context.SaveChanges();

                        return subdomain;
                    }
                }
            }

            return null;
        }

        public static bool EditSubdomainName(int subdomainId, string name)
        {
            using (var context = new ApplicationDbContext())
            {
                Subdomain subdomain = context.Subdomains.FirstOrDefault(sd => sd.Id == subdomainId);
                if ((subdomain != null) && (!context.Subdomains.Any(sd => (sd.Name.ToLower() == name.ToLower()) && (sd.DomainId == subdomain.DomainId))))
                {
                    subdomain.Name = name;
                    context.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public static bool RemoveSubdomainById(int id)
        {
            using (var context = new ApplicationDbContext())
            {

                var domain = context.Subdomains.FirstOrDefault(x => x.Id == id);
                if (domain != null)
                {
                    context.Subdomains.Remove(domain);
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        internal static List<Question> GetQuestionsForSubdomainById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var subdomain = context.Subdomains.Include("Questions").Where(x => x.Id == id).FirstOrDefault();
                if (subdomain != null)
                {
                    return subdomain.Questions.ToList();
                }
                return null;
            }
        }
    }
}