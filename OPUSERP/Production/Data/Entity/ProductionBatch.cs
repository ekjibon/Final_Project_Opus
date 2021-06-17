using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Data.Entity
{
    [Table("ProductionBatchs", Schema = "PROD")]
    public class ProductionBatch:Base
    {
        public int? productionPlanId { get; set; }
        public ProductionPlan productionPlan { get; set; }

        public string batchNumber { get; set; }

        public decimal? quantity { get; set; }

        public DateTime? startDate { get; set; }

        public decimal? perDayTargetQuantity { get; set; }

        public DateTime? expectedEndDate { get; set; }
    }
}
