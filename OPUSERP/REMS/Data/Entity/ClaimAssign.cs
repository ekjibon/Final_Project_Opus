using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.REMS.Data.Entity
{
    [Table("ClaimAssign", Schema = "REMS")]
    public class ClaimAssign:Base
    {
        public int? claimId { get; set; }
        public ClaimRegister claim { get; set; }

        public DateTime? assignDate { get; set; }
        
        public string problemDescription { get; set; }

        public int? assignTypeId { get; set; }
        public AssignType assignType { get; set; }

        public int? assignUserId { get; set; }

        public int? organizationId { get; set; }
        public Organization organization { get; set; }

        public string remarks { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string deliveryMode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string secretCode { get; set; }

    }
}
