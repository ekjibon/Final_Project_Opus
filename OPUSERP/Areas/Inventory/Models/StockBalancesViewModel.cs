
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Inventory.Models
{
    public class StockBalancesViewModel
    {
        public decimal? stockinBalance { get; set; }
        public decimal? stockOutBalance { get; set; }
        public IEnumerable<StockDetails> stocksIn { get; set; }
        public IEnumerable<StockDetails> stocksout { get; set; }
    }
}
