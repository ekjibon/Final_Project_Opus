using System;

namespace OPUSERP.Areas.Accounting.Models
{
    public class GetVoucherListForDeleteViewModel
    {
        public int? Id { get; set; }

        public string voucherNo { get; set; }
        public string refNo { get; set; }
        public DateTime? voucherDate { get; set; }

        public int? voucherTypeId { get; set; }
        
        public string remarks { get; set; }

        
        public decimal? voucherAmount { get; set; }

        public int? isPosted { get; set; }

        public int? fiscalYearId { get; set; }

        public int? taxYearId { get; set; }

        public int? companyId { get; set; }

        public int? fundSourceId { get; set; }
       
       
        public string accountName { get; set; }
        

        public int? specialBranchUnitId { get; set; }
        
    }
}
