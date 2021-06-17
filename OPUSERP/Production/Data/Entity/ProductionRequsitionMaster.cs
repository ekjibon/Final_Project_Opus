using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Data.Entity
{
    [Table("ProductionRequsitionMasters", Schema = "PROD")]
    public class ProductionRequsitionMaster:Base
    {
        public int? productionPlanId { get; set; }
        public ProductionPlan productionPlan { get; set; }
        public int? productionBatchId { get; set; }
        public ProductionBatch productionBatch { get; set; }

        public string requsitionNumber { get; set; }

        public DateTime? date { get; set; }

        public decimal? requisitionQty { get; set; }

        public int? isStockClose { get; set; }

        public int? isClose { get; set; }
    }
}
