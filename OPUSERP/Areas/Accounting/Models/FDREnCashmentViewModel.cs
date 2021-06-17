using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class FDREnCashmentViewModel
    {
        public int? ID { get; set; }
        public int? FDRID { get; set; }
        public string EncashDate { get; set; }
        public decimal? ProvisionAmount { get; set; }
        public decimal? ReceiveAmount { get; set; }
        public decimal? DiffAmount { get; set; }
        public decimal? PercentDiff { get; set; }
        public decimal? ExciseDuty { get; set; }
        public int? PrincipalLedID { get; set; }
        public int? InterestLedID { get; set; }
        public decimal? AdvanceTax { get; set; }
        public decimal? TotalAmount { get; set; }
        public string MaturityStatus { get; set; }
        public decimal? PrematurePercent { get; set; }
        public string Remarks { get; set; }

        public string accountName { get; set; }
        public string AccountName { get; set; }
        public int? principleBankId { get; set; }
        public string principleBankAccountNumber { get; set; }
        public int? interestBankBankId { get; set; }
        public string interestBankAccountNumber { get; set; }

        public decimal? PrincipalAmount { get; set; }

        public IEnumerable<BankBranchDetails> bankBranchDetails { get; set; }
        public IEnumerable<LedgerRelation> ledgerRelations { get; set; }
        public IEnumerable<BankChargeMaster> bankChargeMasters { get; set; }
        public IEnumerable<FDRCalFormula> fDRCalFormulas { get; set; }
        public IEnumerable<FDRInvestmentALLView> fDRInvestmentALLViews { get; set; }
        public IEnumerable<Bank> banks { get; set; }
        public IEnumerable<GetFDRInformationForEncashmentViewModel> getFDRInformationForEncashmentViewModels { get; set; }
        public IEnumerable<GetListOfFDREncashmentWithFDRViewModel> getListOfFDREncashmentWithFDRViewModels { get; set; }
    }
}
