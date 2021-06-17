using OPUSERP.Production.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Inventory.Models
{
    public class ProductionStockOutModel
    {
        public int? stockMasterId { get; set; }
        public IEnumerable<ProductionRequsitionMaster> productionRequsitionMasters { get; set; }
        public ProductionRequsitionMaster productionRequsition { get; set; }
        public IEnumerable<ProductionRequsitionDetails> requsitionDetails { get; set; }
    }
}
