using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class BillCollectionSPViewModel
    {      
        public int? Id { get; set; }
        public int? billCollectionId { get; set; }
        public int? approvedforCroId { get; set; }
        public int? bankBranchDetailsId { get; set; }
        public int? ratingYearId { get; set; }
        public string ratingYearName { get; set; }
        public string ratingTypeName { get; set; }

        public string billNo { get; set; }
        public string billDate { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? dueAmount { get; set; }
        public decimal? collectedAmount { get; set; }
        public string leadName { get; set; }
        public string leadOwner { get; set; }
        public string agreementDate { get; set; }
        public string agreementNo { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string rbankName { get; set; }
        public string rbranchName { get; set; }       
        public string remarks { get; set; }

        public decimal? discount { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? mobileBankAmount { get; set; }
        public decimal? payOrderAmount { get; set; }
        public decimal? challanReceiptVat { get; set; }
        public decimal? challanReceiptTax { get; set; }
        public decimal? receiveTotalAmount { get; set; }
        public int? paymentModeId { get; set; }
        public string paymentModeName { get; set; }
        public string chequeNo { get; set; }
        public string receivedDate { get; set; }
        public string moneyReceipt { get; set; }
        public string vatCategoryName { get; set; }
        public decimal? vatAmount { get; set; }
        public int? bankBranchDetailsIdCollection { get; set; }
        public string Validtill { get; set; }
        public string ratingDate { get; set; }
        public string reportPrintDate { get; set; }
        public string jobStatusName { get; set; }
    }
}
