using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}