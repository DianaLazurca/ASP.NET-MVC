namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Script.Serialization;

    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            EvaluationAnswers = new HashSet<EvaluationAnswer>();
            EvaluationJustifications = new HashSet<EvaluationJustification>();
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }

        public int SubdomainId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Justification { get; set; }

        public bool IsMultiple { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        [ScriptIgnore]
        public virtual ICollection<EvaluationAnswer> EvaluationAnswers { get; set; }

        [ScriptIgnore]
        public virtual ICollection<EvaluationJustification> EvaluationJustifications { get; set; }
        [ScriptIgnore]
        public virtual Subdomain Subdomain { get; set; }

        [ScriptIgnore]
        public virtual ICollection<Test> Tests { get; set; }

        public override bool Equals(object obj)
        {

            if (obj == null)
            {
                return false;
            }

            Question q = obj as Question;
            if ((System.Object)q == null)
            {
                return false;
            }


            return Id == q.Id;
        }
    }
}
