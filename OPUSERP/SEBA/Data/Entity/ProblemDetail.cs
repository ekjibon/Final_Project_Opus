using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SEBA.Data.Entity
{
    [Table("ProblemDetail", Schema = "SEBA")]
    public class ProblemDetail : Base
    {
        public int? problemMasterId { get; set; }
        public ProblemMaster problemMaster { get; set; }

        public int? customerProductWarrantyId { get; set; }
        public CustomerProductWarranty customerProductWarranty { get; set; }
        
        [MaxLength(300)]
        public string problemTitle { get; set; }
        public string description { get; set; }
        
        
    }
}
