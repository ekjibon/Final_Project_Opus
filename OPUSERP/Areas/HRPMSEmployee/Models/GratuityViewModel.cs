using OPUSERP.HRPMS.Data.Entity.Gratuity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class GratuityViewModel
    {
        public int Id { get; set; }

        public int sbuId { get; set; }
        
        public int isJoiningDate { get; set; }

        public int daysDIv { get; set; }

        public int roundMode { get; set; }

        public int?[] FromYear { get; set; }
        public int?[] ToYear { get; set; }
        public decimal?[] Times { get; set; }

        public IEnumerable<SpecialBranchUnit> branches { get; set; }
        public IEnumerable<GratuityPolicyDetail> gratuityPolicyDetails { get; set; }
    }
}
