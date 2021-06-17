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
    public class GetAgreementForCROratingViewModel
    {
        public int? Id { get; set; }
        public string leadName { get; set; }
        public string agreementTypeName { get; set; }
        public string remarks { get; set; }
        public int? agreementId { get; set; }
        public string agreementNo { get; set; }
        public string agreementCategoryName { get; set; }
        public string agreementOwner { get; set; }
        public string ratingTypeName { get; set; }
        public string ratingYearName { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
    }
}
