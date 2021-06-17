namespace OPUSERP.Areas.Accounting.Models
{
    public class FDRMaturityStatusForRenewalViewModel
    {
        public string ftInstructionNo { get; set; }
        public string ftSendTo { get; set; }
        public string bankName { get; set; }
        public int? fdrDetailsId { get; set; }
        public int? fdrInvesmentMasterId { get; set; }
        public int? renewfdrDetailsId { get; set; }
        public int? renewfdrMasterId { get; set; }
        public int? bankChargeMasterId { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public string destinationType { get; set; }
        public string desitinationBank { get; set; }
        public string destinationBranch { get; set; }
        public string sOF { get; set; }
        public string accountNumber { get; set; }
        public string chequeNo { get; set; }
        public string maturityStatus { get; set; }
        public int? linkledgerRelationId { get; set; }
        public int? receiveLinkLedgerID { get; set; }
        public string formulaType { get; set; }
        public string typeText { get; set; }
        public string accountName { get; set; }
        public string receiveAccountName { get; set; }
        public string nOI { get; set; }
        public string branchName { get; set; }
        public string chequeFileName { get; set; }
        public string chequeUpload { get; set; }
        public string ftDate { get; set; }
        public string openDate { get; set; }
        public string maturityDate { get; set; }
        public string tenure { get; set; }
        public string tenureType { get; set; }
        public int? fdrStatus { get; set; }
        public int? approvalStatus { get; set; }
        public string uploadDoc { get; set; }
        public string docName { get; set; }
        public string uploadMemo { get; set; }
        public string memoName { get; set; }

        public int? isEncashment { get; set; }
        public decimal? totalInterest { get; set; }
        public string npbAddress { get; set; }
        public int? encashRecLedgerRelId { get; set; }
        public string encashRecAccount { get; set; }
        public decimal? encashBankCharge { get; set; }

        public string statusType { get; set; }
    }
}
