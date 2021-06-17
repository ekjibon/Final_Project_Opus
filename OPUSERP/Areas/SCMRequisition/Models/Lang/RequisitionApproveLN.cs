using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models.Lang
{
    [NotMapped]
    public class RequisitionApproveLN
    {
        public string reqNo { get; set; }
        public string reqDate { get; set; }
        public string targetDate { get; set; }
        public string justification { get; set; }
        public string subject { get; set; }
        public string bracsubject { get; set; }
        public string reqDept { get; set; }
        public string reqType { get; set; }
        public string supplier { get; set; }
        public string isPostPR { get; set; }
        public string statusInfo { get; set; }

        public string itemDetails { get; set; }
        public string itemId { get; set; }
        public string itemSpecificationId { get; set; }
        public string lastPrice { get; set; }
        public string reqQty { get; set; }
        public string reqRate { get; set; }
        public string totalAmount { get; set; }
        public string itemDetailsList { get; set; }
        public string unit { get; set; }

        public string title { get; set; }
        public string listTitle { get; set; }
        public string action { get; set; }
    }
}
