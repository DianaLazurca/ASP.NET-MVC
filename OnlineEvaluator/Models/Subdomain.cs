namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Script.Serialization;

    public partial class Subdomain
    {
        public Subdomain()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        public int DomainId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [ScriptIgnore]
        public virtual Domain Domain { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
