using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class BillGenerateViewModel
    {
        public int billGenerateId { get; set; }
        public int? approvedforCroId { get; set; }
        public int? bankBranchDetailsId { get; set; }
        public string billNo { get; set; }       
        public string invoiceNo { get; set; }
        public DateTime? billDate { get; set; }
        public decimal? ratingAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? agreementAmount { get; set; }
        public string remarks { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? invoiceAmount { get; set; }
        public string invoiceOrder { get; set; }

        //public IEnumerable<ApprovedforCro> clients { get; set; }
        public IEnumerable<BillGenerateSPViewModel> clients { get; set; }
        public IEnumerable<BillGenerate> billGenerates { get; set; }
        public IEnumerable<BankBranchDetails> bankBranchDetails { get; set; }

        //Report
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<BillGenerateReportViewModel> billGenerateReportViewModels { get; set; }
    }
}
