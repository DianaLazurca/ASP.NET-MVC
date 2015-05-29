namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Script.Serialization;

    public partial class EvaluationJustification
    {
        public int Id { get; set; }

        public int EvaluationId { get; set; }

        public int QuestionId { get; set; }

        public string Justification { get; set; }
        [ScriptIgnore]
        public virtual Evaluation Evaluation { get; set; }
        [ScriptIgnore]
        public virtual Question Question { get; set; }
    }
}
