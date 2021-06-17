using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Areas.Production.Models
{
    public class ProductionPlanViewModel
    {
        public int planId { get; set; }

        public string planNumber { get; set; }

        public DateTime? planDate { get; set; }

        public DateTime? targetDate { get; set; }

        public int? bOMMasterId { get; set; }

        public decimal? quantity { get; set; }

        public decimal? batchQuantity { get; set; }

        public DateTime? startDate { get; set; }

        public decimal? perDayTargetQuantity { get; set; }

        public decimal?[] perDayTargetQuantitys { get; set; }
        public decimal?[] batchQuantitys { get; set; }
        public DateTime?[] batchDates { get; set; }
        public DateTime?[] batchTargetDate { get; set; }

        public IEnumerable<BOMMaster> bOMMasters { get; set; }
        public IEnumerable<ProductionPlan> productionPlans { get; set; }
    }
}
