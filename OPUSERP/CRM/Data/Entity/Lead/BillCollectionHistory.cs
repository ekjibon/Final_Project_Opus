using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("BillCollectionHistory", Schema = "CRM")]
    public class BillCollectionHistory : Base
    {
        public int? billCollectionId { get; set; }
        public BillCollection billCollection { get; set; }

        [MaxLength(250)]
        public string chequeNo { get; set; }
        public DateTime? receivedDate { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? mobileBankAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? discount { get; set; }
        public string moneyReceipt { get; set; }
        public decimal? challanReceiptVat { get; set; }
        public decimal? challanReceiptTax { get; set; }


    }
}
