using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMPurchaseOrder.Models
{
    public class CSItemVM
    {
        public int? itemId { get; set; }
        public int? csDetailsId { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string itemSpec { get; set; }
        public string unit { get; set; }
        public decimal? qnt { get; set; }
        public decimal? UnorderQnt { get; set; }
        public decimal? CSUnitPrice { get; set; }
        public decimal? PRQnt { get; set; }
    }
}
