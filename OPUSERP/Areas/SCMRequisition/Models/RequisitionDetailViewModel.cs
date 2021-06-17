using System;

namespace OPUSERP.Areas.SCMRequisition.Models
{
    public class RequisitionDetailViewModel
    {
        public int Id { get; set; }
        public int requisitionMasterId { get; set; }
        
        public decimal? reqQty { get; set; }
        
        public decimal? reqRate { get; set; }

        public DateTime? targetDate { get; set; }

        public string justification { get; set; }

        public int? teamMemberId { get; set; }

        public DateTime? jobAssignDate { get; set; }

        public int? itemSpecificationId { get; set; }
        public string specificationName { get; set; }

        public int? itemId { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }

        public int? unitId { get; set; }
        public string unitName { get; set; }

        public int? teamMasterId { get; set; }

        public decimal? unOrderQty { get; set; }
        public decimal? orderQty { get; set; }

        public decimal? total { get; set; }
        public decimal? qumQTY { get; set; }
        public decimal? lastPrice { get; set; }
    }
}
