using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Gratuity
{
    [Table("GratuityPolicyMaster", Schema = "HR")]
    public class GratuityPolicyMaster : Base
    {
        public int sbuId { get; set; }
        public SpecialBranchUnit sbu { get; set; }
        public decimal daysDIv { get; set; }
        public int roundMode { get; set; }
        public int isJoiningDate { get; set; }
    }
}
