namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EvaluationAnswer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EvaluationId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnswerId { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool GivenAnswer { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Evaluation Evaluation { get; set; }

        public virtual Question Question { get; set; }
    }
}
