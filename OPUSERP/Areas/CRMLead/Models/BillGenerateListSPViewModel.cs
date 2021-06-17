using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class BillGenerateListSPViewModel
    {      
        public int? Id { get; set; }
        public int? approvedforCroId { get; set; }
        public int? bankBranchDetailsId { get; set; }
        public int? ratingYearId { get; set; }
        public string ratingYearName { get; set; }
        public string ratingTypeName { get; set; }

        public string billNo { get; set; }
        public string billDate { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? dueAmount { get; set; }
        public string leadName { get; set; }
        public string leadOwner { get; set; }
        public string agreementDate { get; set; }
        public string agreementNo { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }       
        public string remarks { get; set; }
        public string invoiceOrder { get; set; }
        public decimal? invoiceAmount { get; set; }
        public string CbankName { get; set; }
        public string CbranchName { get; set; }
        public string ratingCategoryName { get; set; }
        public string agreementCategoryName { get; set; }
     
    }
}
