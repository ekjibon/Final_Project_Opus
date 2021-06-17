using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("BillCollection", Schema = "CRM")]
    public class BillCollection : Base
    {
        public int? billGenerateId { get; set; }
        public BillGenerate billGenerate { get; set; }

        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode{ get; set; }

        public int? bankBranchDetailsId { get; set; }
        public BankBranchDetails bankBranchDetails { get; set; }
        [MaxLength(250)]
        public string chequeNo { get; set; }
        public DateTime? receivedDate { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? mobileBankAmount { get; set; }
        public decimal? payOrderAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? discount { get; set; }
        public string moneyReceipt { get; set; }
        public decimal? challanReceiptVat { get; set; }
        public decimal? challanReceiptTax { get; set; }
    }
}
