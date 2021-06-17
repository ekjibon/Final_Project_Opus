namespace OPUSERP.Areas.Accounting.Models
{
    public class GetFDRInformationForEncashmentViewModel
    {

        public string fTInstructionNo { get; set; }
        public string fTSendTo { get; set; }
        public string bankName { get; set; }
        public int? fDRID { get; set; }
        public int? fDRInvesmentMasterId { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public string destinationType { get; set; }
        public string desitinationBank { get; set; }
        public string destinationBranch { get; set; }
        public string sOF { get; set; }
        public string accountNo { get; set; }
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
        public int? fDRStatus { get; set; }
        public int? approvalStatus { get; set; }
        public string uploadDoc { get; set; }
        public string docName { get; set; }
        public string uploadMemo { get; set; }
        public string memoName { get; set; }
        public int? isEncashment { get; set; }
        public decimal? totalInterest { get; set; }
        public string nPBAddress { get; set; }

    }
}
