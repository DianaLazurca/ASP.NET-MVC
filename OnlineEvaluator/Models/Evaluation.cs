namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Evaluation
    {
        public Evaluation()
        {
            EvaluationAnswers = new HashSet<EvaluationAnswer>();
            EvaluationJustifications = new HashSet<EvaluationJustification>();
        }

        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }

        public int TestId { get; set; }

        public DateTime StartDate { get; set; }

        public int Duration { get; set; }

        public int Points { get; set; }

        public virtual ICollection<EvaluationAnswer> EvaluationAnswers { get; set; }

        public virtual ICollection<EvaluationJustification> EvaluationJustifications { get; set; }

        public virtual Test Test { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
