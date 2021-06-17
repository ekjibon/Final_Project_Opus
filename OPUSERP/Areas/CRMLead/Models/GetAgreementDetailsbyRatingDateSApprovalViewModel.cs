using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class GetAgreementDetailsbyRatingDateSApprovalViewModel
    {
        
        public int? Id { get; set; }
        public int? ratingYearId { get; set; }
        public int? vatCategoryId { get; set; }
        public int? taxCategoryId { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? ratingAmount { get; set; }
        public decimal? totalAmount { get; set; }
        public DateTime ratingDate { get; set; }
        public decimal? agreementAmount { get; set; }
        public string leadName { get; set; }
        public int? agreementId { get; set; }
        public string ratingTypeName { get; set; }
        public string ratingYearName { get; set; }
        public string leadOwner { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }

    }
}
