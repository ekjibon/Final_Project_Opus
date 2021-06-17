using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.OtherSales.Data.Entity.Sales;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("BillGenerate", Schema = "CRM")]
    public class BillGenerate : Base
    {        
        public int? approvedforCroId { get; set; }
        public ApprovedforCro approvedforCro { get; set; }

        public int? bankBranchDetailsId { get; set; }
        public BankBranchDetails bankBranchDetails { get; set; }

        [MaxLength(250)]
        public string billNo { get; set; }
        [MaxLength(30)]
        public string invoiceNo { get; set; }
        public DateTime? billDate { get; set; }
        public decimal? ratingAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? advanceAmount { get; set; }
        public string remarks { get; set; }
        public string invoiceOrder { get; set; }
        public decimal? invoiceAmount { get; set; }

        public int? salesInvoiceMasterId { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }

        public int? leadssId { get; set; }
        public Leads leadss { get; set; }

        public int? agreementDetailssId { get; set; }
        public AgreementDetails agreementDetailss { get; set; }

        public int? agreementtId { get; set; }
        public Agreement agreementt { get; set; }
    }
}
