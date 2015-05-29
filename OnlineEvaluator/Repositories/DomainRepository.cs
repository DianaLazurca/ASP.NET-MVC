using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineEvaluator.Repositories
{
    public static class DomainRepository
    {
        public static Domain AddNewDomain(String domainName)
        {
            using (var context = new ApplicationDbContext())
            {
                if ((domainName != null) && (!context.Domains.Any(d => d.Name.ToLower() == domainName.ToLower())))
                {
                    Domain domain = new Domain { Name = domainName };
                    context.Domains.Add(domain);
                    context.SaveChanges();

                    return domain;
                }

            }

            return null;
        }

        public static Domain GetDomainById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Domains.Where(x => x.Id == id).SingleOrDefault();
            }
        }
        // sterge si subdomeniile + intrebarile asociate
        public static bool RemoveDomainById(int id)
        {            

            using (var context = new ApplicationDbContext())
            {

                var domain = context.Domains.FirstOrDefault(x => x.Id == id);
                if (domain != null)
                {
                    context.Domains.Remove(domain);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
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

        public static List<Subdomain> GetSubdomainsForDomainById(int id)
        {

            using (var context = new ApplicationDbContext())
            {
                Domain domain = context.Domains.Include("Subdomains")
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (domain != null)
                {
                    return domain.Subdomains.ToList();
                }
            }
            return null;
        }

        public static bool EditDomainName(int domainId, string name)
        {
            using (var context = new ApplicationDbContext())
            {
                Domain domain = context.Domains.FirstOrDefault(d => d.Id == domainId);
                if ((domain != null) && (!context.Domains.Any(d => d.Name.ToLower() == name.ToLower())))
                {
                    domain.Name = name;
                    context.SaveChanges();

                    return true;
                }

                return false;
            }
        }
    }
}