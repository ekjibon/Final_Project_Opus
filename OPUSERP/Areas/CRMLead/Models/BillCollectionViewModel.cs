using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class BillCollectionViewModel
    {
        public int? billCollectionId { get; set; }
        public int? billGenerateId { get; set; }


        public int? paymentModeId { get; set; }
        public int? bankBranchDetailsId { get; set; }


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

        public IEnumerable<BillGenerate> billGenerates { get; set; }
        public IEnumerable<BillCollection> billCollections { get; set; }
        public BillCollection billCollection { get; set; }
        public IEnumerable<BillCollectionHistory> billCollectionHistories{ get; set; }
        public IEnumerable<PaymentMode> paymentModes { get; set; }
        public IEnumerable<BankBranchDetails> bankBranchDetails { get; set; }


    }
}
