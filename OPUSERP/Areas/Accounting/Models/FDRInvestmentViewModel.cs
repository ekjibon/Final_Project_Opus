using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.CRM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class FDRInvestmentViewModel
    {
        public int? fDRInvesmentMasterId { get; set; }

        public int? bankId { get; set; }
        public string bankAccountNo { get; set; }
        public string bankBranchName { get; set; }
        public string FTInstructionNo { get; set; }
        public string FTSendTo { get; set; }
        public DateTime? FTDate { get; set; }
        public string NOI { get; set; }
        public string SOF { get; set; }
        public int? IsJournal { get; set; }
        public int FDRStatus { get; set; }
        public int? Status { get; set; }
        public int? IsRenew { get; set; }
        public int? ParentID { get; set; }
        
        public string RefNo { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string DestinationType { get; set; }
        public int? detailsbankId { get; set; }
        public string DestinationBranch { get; set; }
        public string ChequeNo { get; set; }
        public string FormulaType { get; set; }
        public string MaturityStatus { get; set; }
        public int ApprovalStatus { get; set; }
        public int? ParentFDRID { get; set; }
        public decimal? TotalInterest { get; set; }
        public string Tenure { get; set; }
        public string TenureType { get; set; }
        public string NPBAddress { get; set; }
        public decimal? encashBankCharge { get; set; }


        public IEnumerable<Bank> bankBranchDetails { get; set; }
        public IEnumerable<LedgerRelation> ledgerRelations { get; set; }
        public IEnumerable<BankChargeMaster> bankChargeMasters { get; set; }
        public IEnumerable<FDRCalFormula> fDRCalFormulas { get; set; }
        public IEnumerable<FDRInvestmentALLView> fDRInvestmentALLViews { get; set; }
        public IEnumerable<FDRInvestmentDetail> fDRInvestmentDetails { get; set; }

    }
}
