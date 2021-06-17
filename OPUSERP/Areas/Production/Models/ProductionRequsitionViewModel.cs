using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Areas.Production.Models
{
    public class ProductionRequsitionViewModel
    {
        public int? requisitionId { get; set; }
        public int? bOMId { get; set; }
        public int? planId { get; set; }
        public int? BatchId { get; set; }
        public decimal? requisitionQty { get; set; }

        public string requisitionNo { get; set; }

        public DateTime? requisitionDate { get; set; }
        
        public string remarks { get; set; }

        public int?[] productionDetailId { get; set; }
        public int?[] bOMDetailId { get; set; }
        public decimal?[] qty { get; set; }
        public decimal?[] defaultPrice { get; set; }
        public int?[] defaultSupplierId { get; set; }

        public IEnumerable<ProductionRequsitionMaster> productionRequsitionMasters { get; set; }
        public IEnumerable<ProductionRequsitionDetails> productionRequsitionDetails { get; set; }
        public IEnumerable<ProductionPlan> productionPlans { get; set; }

    }
}
