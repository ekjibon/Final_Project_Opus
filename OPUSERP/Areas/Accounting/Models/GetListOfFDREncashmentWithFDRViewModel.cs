namespace OPUSERP.Areas.Accounting.Models
{
    public class GetListOfFDREncashmentWithFDRViewModel
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
        public string principalLedger { get; set; }
        public string InterestLedger { get; set; }
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
        public decimal? totalInterest { get; set; }
        public string nPBAddress { get; set; }
        
        public string encashDate { get; set; }
        public int? Id { get; set; }
        public decimal? provisionAmount { get; set; }
        public decimal? receiveAmount { get; set; }
        public decimal? diffAmount { get; set; }
        public decimal? advanceTax { get; set; }
        public decimal? exciseDuty { get; set; }
        public decimal? percentDiff { get; set; }
        public decimal? totalAmount { get; set; }
        public int? typeOfDiff { get; set; }
        public int? principalLedID { get; set; }
        public int? interestLedID { get; set; }
        public int? status { get; set; }
        public string approverStatus { get; set; }
        public string encashStatus { get; set; }
        public string remarks { get; set; }

    }
}
