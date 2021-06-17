namespace OPUSERP.Areas.Accounting.Models
{
    public class FDRReportViewModel
    {
        public int? fdrDetailsId { get; set; }
        public int? fdrInvesmentMasterId { get; set; }
        public string refNo { get; set; }
        public string ftInstructionNo { get; set; }
        public string ftSendTo { get; set; }
        public string nOI { get; set; }
        public string sOF { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public decimal? totalAmount { get; set; }
        public string ftDate { get; set; }
        public string accountName { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string accountNumber { get; set; }
        public string openDate { get; set; }
        public string maturityDate { get; set; }
        public string formulaType { get; set; }
        public string typeText { get; set; }
        public int? isRenew { get; set; }
        public string statusType { get; set; }
        public string receiveAccountName { get; set; }    
        public string destinationType { get; set; }
        public string desitinationBank { get; set; }
        public string destinationBranch { get; set; } 
        public string chequeNo { get; set; }
        public string totalApproveFTWord { get; set; }
        public int? totalFT { get; set; }
        public string amountToWord { get; set; }        
        public string tenure { get; set; }
        public string address { get; set; }
        public string motherBank { get; set; }       
        public string motherBranch { get; set; }
        public string motherAddress { get; set; }

        public decimal? toDaysProvision { get; set; }
        public decimal? cumalitiveProvision { get; set; }
        public decimal? totalProvision { get; set; }

        public decimal? receiveAmount { get; set; }
    }
}
