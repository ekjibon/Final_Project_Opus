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
    public class OperationMasterSPBlockUnViewModel
    {
        public int Id { get; set; }
        public string agreementNo { get; set; }
        public string leadName { get; set; }
        public string agreementTypeName { get; set; }
        public string agreementCategoryName { get; set; }
        
        public string agreementOwner { get; set; }
        public DateTime? reportDate { get; set; }
        public string assignTeamLeader { get; set; }
        public string assignTo { get; set; }




    }
}
