namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Test
    {
        public Test()
        {
            Evaluations = new HashSet<Evaluation>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        public int DomainId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public int Duration { get; set; }

        public virtual Domain Domain { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
