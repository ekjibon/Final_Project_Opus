using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMStock.Models
{
    public class StockItemViewModel
    {
        public int? Id { get; set; }
        public int? itemSpecificationId { get; set; }
        public decimal? quantity { get; set; }
        public decimal? dueQuantity { get; set; }
        public decimal? rate { get; set; }
        public string itemName { get; set; }
        public string itemSpecificationName { get; set; }
    }
}
