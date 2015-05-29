namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Script.Serialization;

    public partial class EvaluationAnswer
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }

        public int QuestionId { get; set; }

        public int AnswerId { get; set; }

        public bool GivenAnswer { get; set; }
        [ScriptIgnore]
        public virtual Answer Answer { get; set; }
        [ScriptIgnore]
        public virtual Evaluation Evaluation { get; set; }
        [ScriptIgnore]
        public virtual Question Question { get; set; }
    }
}
