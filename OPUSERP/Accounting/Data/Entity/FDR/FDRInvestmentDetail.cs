using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Data.Entity.FDR
{
    [Table("FDRInvestmentDetail", Schema = "ACCOUNT")]
    public class FDRInvestmentDetail:Base
    {
        public int? fDRInvesmentMasterId { get; set; }
        public FDRInvesmentMaster fDRInvesmentMaster { get; set; }

        public string RefNo { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string DestinationType { get; set; }

        public int? bankId { get; set; }
        public Bank bank { get; set; }

        public string DestinationBranch { get; set; }
        public string ChequeNo { get; set; }

        public int? LinkledgerRelationId { get; set; }
        public LedgerRelation LinkledgerRelation { get; set; }

        public int? ReceiveLinkLedgerId { get; set; }
        public LedgerRelation ReceiveLinkLedger { get; set; }

        public string FormulaType { get; set; }
        public string ChequeUpload { get; set; }
        public string UploadDoc { get; set; }
        public string UploadMemo { get; set; }
        public string MaturityStatus { get; set; }
        public int ApprovalStatus { get; set; }
        public int? ParentFDRID { get; set; }
        public decimal? TotalInterest { get; set; }
        public string Tenure { get; set; }
        public string TenureType { get; set; }
        public string NPBAddress { get; set; }

        public int? encashRecLedgerRelId { get; set; }
        public LedgerRelation encashRecLedgerRel { get; set; }
        public decimal? encashBankCharge { get; set; }
    }
}
