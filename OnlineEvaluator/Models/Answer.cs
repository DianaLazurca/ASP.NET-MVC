namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        public Answer()
        {
            EvaluationAnswers = new HashSet<EvaluationAnswer>();
        }

        public int Id { get; set; }

        public int QuestionId { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsCorect { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<EvaluationAnswer> EvaluationAnswers { get; set; }
    }
}
