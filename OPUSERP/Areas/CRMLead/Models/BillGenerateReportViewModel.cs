using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class BillGenerateReportViewModel
    {       


       
        public string agreementNo { get; set; }
        public string ratingTypeName { get; set; }
        public string ratingType { get; set; }
        public string agreementDate { get; set; }
        public string attentionDesignation { get; set; }
        public string attentionName { get; set; }
        public string agreementCategoryName { get; set; }
        public string designation { get; set; }
        public string expiredDate { get; set; }

        public string leadName { get; set; }
        public string leadNumber { get; set; }
        public string addres { get; set; }
        public decimal? ratingAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public string inword { get; set; }
        public string billNo { get; set; }

        public string billDate { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string accountNo { get; set; }

        public decimal? vatRate { get; set; }
        public decimal? taxRate { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? advanceAmount { get; set; }
        public decimal? finalPaymentDue { get; set; }
        public string invoiceOrder { get; set; }
        public string vatCategoryName { get; set; }
    }
}
