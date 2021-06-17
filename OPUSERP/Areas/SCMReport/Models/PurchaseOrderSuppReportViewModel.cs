using System;

namespace OPUSERP.Areas.SCMReport.Models
{
    public class PurchaseOrderSuppReportViewModel
    {
       
        public string poNo { get; set; }
        public DateTime? poDate { get; set; }
        public DateTime? reqDate { get; set; }
        public string organizationName { get; set; }
        public string ProjectName { get; set; }
        public string reqNo { get; set; }
        public decimal? VaT { get; set; }
        public decimal? Amount { get; set; }


    }
}
