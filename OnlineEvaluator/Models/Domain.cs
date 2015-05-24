namespace OnlineEvaluator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Domain
    {
        public Domain()
        {
            Subdomains = new HashSet<Subdomain>();
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Subdomain> Subdomains { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
