using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Data.Entity
{
    [Table("ProductionPlans", Schema = "PROD")]
    public class ProductionPlan:Base
    {
        public string planNumber { get; set; }

        public DateTime? planDate { get; set; }

        public DateTime? targetDate { get; set; }

        public int? bOMMasterId { get; set; }
        public BOMMaster bOMMaster { get; set; }

        public decimal? quantity { get; set; }
    }
}
