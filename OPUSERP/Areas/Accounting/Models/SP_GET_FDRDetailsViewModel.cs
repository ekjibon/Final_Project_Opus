using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class SP_GET_FDRDetailsViewModel
    {
        public string ftInstructionNo { get; set; }
        public string ftSendTo { get; set; }
        public string bankName { get; set; }
        public int? Id { get; set; }
        public int? fTMasterId { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public string destinationType { get; set; }
        public string desitinationBank { get; set; }
        public int? desitinationBankId { get; set; }
        public string destinationBranch { get; set; }
        public int? FDRStatus { get; set; }
        public string chequeNo { get; set; }
        public string MaturityStatus { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? LinkLedgerID { get; set; }
        public int? ReceiveLinkLedgerID { get; set; }
        public string FormulaType { get; set; }
        public string TypeText { get; set; }
        public string accountName { get; set; }
        public string ReceiveAccountName { get; set; }
        public string ChequeFileName { get; set; }
        public string ChequeUpload { get; set; }        
        public string OpenDate { get; set; }
        public string MaturityDate { get; set; }
        public string NPBAddress { get; set; }
        public string Tenure { get; set; }
        public string TenureType { get; set; }
        public string UploadDoc { get; set; }

        public string DocName { get; set; }
        public string UploadMemo { get; set; }
        public string MemoName { get; set; }
        public decimal? TotalInterest { get; set; }
    }
}
