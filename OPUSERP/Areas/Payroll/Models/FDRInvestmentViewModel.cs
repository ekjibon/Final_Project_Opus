using Microsoft.AspNetCore.Http;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Payroll.Data.Entity.PF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class FDRInvestmentViewModel
    {
        public int? FdrInvestmentId { get; set; }
        public string Ftno { get; set; }
        public string FromBank { get; set; }
        public string FromBranch { get; set; }
        public string AccountNumber { get; set; }
        public string ToBank { get; set; }
        public string ToBranch { get; set; }
        public string ChequeNo { get; set; }
        public IFormFile ChequeFile { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Rate { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public int? Tenure { get; set; }
        public decimal? Interest { get; set; }
        public IEnumerable<FdrInvestment> fdrInvestments { get; set; }
        public FdrInvestment fdrInvestment { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documentPhotoAttachments { get; set; }
    }
}
