using System;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportNinePointOneCHHViewModel
    {
    
        public string chalanNo { get; set; }
        public string economicCode { get; set; }
       
        public decimal? amount { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public DateTime? paymentDate { get; set; }
    
    }
}
