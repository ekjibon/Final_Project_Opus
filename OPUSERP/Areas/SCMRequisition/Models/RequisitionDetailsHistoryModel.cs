using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
    public class RequisitionDetailsHistoryModel
    {
        public int? reqDetailId { get; set; }
        public int? itemSpecificationId { get; set; }
        public string specificationName { get; set; }
        public string specImageUrl { get; set; }
        public string SKUNumber { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? BalanceQty { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string unitName { get; set; }
        public string LastReqDate { get; set; }
        public decimal? LastQty { get; set; }
        public decimal? BookingQty { get; set; }
    }
}