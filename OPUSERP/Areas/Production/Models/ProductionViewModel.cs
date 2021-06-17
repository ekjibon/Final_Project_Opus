using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Areas.Production.Models
{
    public class ProductionViewModel
    {
        public int? productionId { get; set; }

        public string productionNo { get; set; }

        public DateTime? productionDate { get; set; }

        public int? bOMId { get; set; }

        public int? BatchId { get; set; }

        public int? PlanId { get; set; }

        public int? supplierId { get; set; }

        public decimal? bomQty { get; set; }

        public string remarks { get; set; }
        public string bomItem { get; set; }
        public string supplierName { get; set; }

        public int?[] productionDetailId { get; set; }
        public int?[] bOMDetailId { get; set; }
        public decimal?[] qty { get; set; }
        public decimal?[] defaultPrice { get; set; }
        public int?[] defaultSupplierId { get; set; }
        public decimal?[] wastePercent { get; set; }

        public IEnumerable<ProductionMaster> productionMasters { get; set; }
        public IEnumerable<ProductionPlan> productionPlans { get; set; }
        public ProductionBatch productionBatch { get; set; }
        public IEnumerable<ProductionDetails> productionDetails { get; set; }
        public IEnumerable<BOMMaster> bOMMasters { get; set; }
    }
}
