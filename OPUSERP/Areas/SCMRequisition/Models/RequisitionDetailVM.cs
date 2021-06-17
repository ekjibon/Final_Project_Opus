using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
    public class RequisitionDetailVM
    {
        public string rowSl { get; set; }
        public int Id { get; set; }
        public int requisitionMasterId { get; set; }

        public decimal? reqQty { get; set; }

        public decimal? reqRate { get; set; }

        public DateTime? targetDate { get; set; }

        public string justification { get; set; }
        public string budgetCode { get; set; }

        public int? itemSpecificationId { get; set; }
        public string specificationName { get; set; }

        public int? itemId { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string unitName { get; set; }
        
        public decimal? total { get; set; }
        public decimal? qumQTY { get; set; }
        public decimal? lastPrice { get; set; }
    }
}
