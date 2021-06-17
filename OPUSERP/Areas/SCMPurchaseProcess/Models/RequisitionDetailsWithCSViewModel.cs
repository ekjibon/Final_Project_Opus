using System;

namespace OPUSERP.Areas.SCMPurchaseProcess.Models
{
    public class RequisitionDetailsWithCSViewModel
    {
        public string reqNo { get; set; }

        public DateTime? reqDate { get; set; }

        public string reqDept { get; set; }

        public int? requisitionMasterId { get; set; }

        public int? requisitionDetailId { get; set; }

        public int? itemSpecificationId { get; set; }

        public decimal? reqQty { get; set; }

        public decimal? reqRate { get; set; }

        public decimal? csQty { get; set; }

        public decimal? orderQty { get; set; }

        public decimal? unOrderQty { get; set; }

        public DateTime? targetDate { get; set; }

        public string justification { get; set; }

        public int? teamMemberId { get; set; }

        public int? itemId { get; set; }

        public string itemCode { get; set; }

        public string itemName { get; set; }

        public string itemSpecificationName { get; set; }

        public string unitName { get; set; }

        public DateTime? jobAssignDate { get; set; }

        public string reqStatus { get; set; }

        public string remarks { get; set; }

        //public IEnumerable<CSItemCategory> itemCategories { get; set; }
    }
}
