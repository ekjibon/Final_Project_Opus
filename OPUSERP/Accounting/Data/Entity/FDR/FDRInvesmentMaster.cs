using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Data.Entity.FDR
{
    [Table("FDRInvesment", Schema = "ACCOUNT")]
    public class FDRInvesmentMaster:Base
    {
        public int? bankId { get; set; }
        public Bank bank { get; set; }

        public string bankAccountNo { get; set; }
        public string bankBranchName { get; set; }

        public string FTInstructionNo { get; set; }
        public string FTSendTo { get; set; }
        public DateTime? FTDate { get; set; }
        public string NOI { get; set; }
        public string SOF { get; set; }
        public int? IsJournal { get; set; }
        public int FDRStatus { get; set; } //0=FT Create,1=FT Approve,-1=FT Reject,2=FDR Create,3=FDR Approve,-3=FDR Reject,4=Renew,5=Encashment,6=Partial FT Approve,7=Partial FT Approve
        public int? Status { get; set; }
        public int? IsRenew { get; set; } //0=new,1=renew,2=club,3=endcash
        public int? ParentID { get; set; }
    }
}
