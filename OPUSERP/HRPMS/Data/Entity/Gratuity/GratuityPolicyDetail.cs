using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Gratuity
{
    [Table("GratuityPolicyDetail", Schema = "HR")]
    public class GratuityPolicyDetail : Base
    {
        public int? fromYear { get; set; }
        public int? toYear { get; set; }
        public decimal? Times { get; set; }
        public int gratuityPolicyId { get; set; }
        public GratuityPolicyMaster gratuityPolicy { get; set; }
    }
}
