using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRO.Models
{
    public class OperationMasterSPViewModel
    {
        public int Id { get; set; }
        public string agreementNo { get; set; }
        public string leadName { get; set; }
        public string agreementTypeName { get; set; }
        public string agreementCategoryName { get; set; }
        
        public string agreementOwner { get; set; }
        public string reportDate { get; set; }

        public string bankName { get; set; }
        public string branchName { get; set; }
        public int? agreementId { get; set; }



    }
}
