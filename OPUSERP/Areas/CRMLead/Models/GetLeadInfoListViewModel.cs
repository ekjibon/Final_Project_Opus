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
    public class GetLeadInfoListViewModel
    {
        
        public int? Id { get; set; }
        public string leadName { get; set; }
        public string leadNumber { get; set; }
        public string leadOwner { get; set; }
        public string leadShortName { get; set; }
        public string sectorName { get; set; }
        public int? isPersonal { get; set; }
        public string groupName { get; set; }
        public string agreementCategoryName { get; set; }
        public string agreementStatusName { get; set; }
        public string bankName { get; set; }
        public string BranchName { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? expiredDate { get; set; }
        public string maillingAddress { get; set; }
        public string sourceDescription { get; set; }
        public string mobile { get; set; }

    }
}
