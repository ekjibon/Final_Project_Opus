using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class BillGenerateSPViewModel
    {      
        public int? Id { get; set; }

        public string leadName { get; set; }
        public string leadOwner { get; set; }
        public string agreementCategoryName { get; set; }
        public string ratingYearName { get; set; }
        public string ratingTypeName { get; set; }
        public string agreementTypeName { get; set; }
        public string ratingTypeYear { get; set; }
        public string agreementDate { get; set; }
        public string agreementNo { get; set; }
        public decimal? agreementAmount { get; set; }
        public string vatCategoryName { get; set; }
        public decimal? vatRate { get; set; }
        public decimal? ratingAmount { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? taxRate { get; set; }
        public decimal? taxAmount { get; set; }
        public string remarks { get; set; }

        public string bankName { get; set; }
        public string branchName { get; set; }
    }
}
