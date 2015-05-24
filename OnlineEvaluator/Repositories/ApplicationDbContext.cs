using Microsoft.AspNet.Identity.EntityFramework;
using OnlineEvaluator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineEvaluator.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ModelContainer", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<Answer> Answers { get; set; }
        public virtual IDbSet<Domain> Domains { get; set; }
        public virtual IDbSet<Evaluation> Evaluations { get; set; }
        public virtual IDbSet<Question> Questions { get; set; }
        public virtual IDbSet<Subdomain> Subdomains { get; set; }
        public virtual IDbSet<Test> Tests { get; set; }
        public virtual IDbSet<EvaluationAnswer> EvaluationAnswers { get; set; }
        public virtual IDbSet<EvaluationJustification> EvaluationJustifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .HasMany(e => e.EvaluationAnswers)
                .WithRequired(e => e.Answer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Domain>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Domain>()
                .HasMany(e => e.Subdomains)
                .WithRequired(e => e.Domain)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Domain>()
                .HasMany(e => e.Tests)
                .WithRequired(e => e.Domain)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evaluation>()
                .HasMany(e => e.EvaluationAnswers)
                .WithRequired(e => e.Evaluation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evaluation>()
                .HasMany(e => e.EvaluationJustifications)
                .WithRequired(e => e.Evaluation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(e => e.Evaluations)
               .WithRequired(e => e.User)
               .HasForeignKey(e => e.ApplicationUserId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.EvaluationAnswers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.EvaluationJustifications)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Tests)
                .WithMany(e => e.Questions)
                .Map(m => m.ToTable("TestsQuestions").MapLeftKey("QuestionId").MapRightKey("TestId"));

            modelBuilder.Entity<Subdomain>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subdomain>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Subdomain)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.Evaluations)
                .WithRequired(e => e.Test)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}