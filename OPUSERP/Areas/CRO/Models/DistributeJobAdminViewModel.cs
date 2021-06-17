using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.CRMLead.Models;
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
    public class DistributeJobAdminViewModel
    {
        public int operationMasterId { get; set; }
        public int? ircSignatoryId { get; set; }
        public int? employeeId { get; set; }
        public int? serialNo { get; set; }
        public string assignTeam { get; set; }
        public string assignTeamLeader { get; set; }
        public DateTime? assignTeamDate { get; set; }
        public DateTime? assignCoHeadDate { get; set; }
        public string assignCoHead { get; set; }
        public DateTime? assignDate { get; set; }
        public string assignTo { get; set; }
        public DateTime? tLDeclarationDate { get; set; }
        public int? declaration { get; set; }
        public int? jobStatusId { get; set; }
        


        public int?[] agreementId { get; set; }
        public int?[] approvedforCroId { get; set; }
        
        public string[] remarks { get; set; }


        public IEnumerable<Agreement> agreements { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsers { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<OperationMaster> operationMasters { get; set; }
        public IEnumerable<ApprovedforCro>  approvedforCros { get; set; }
        public IEnumerable<GetAgreementForCROratingViewModel> getAgreementForCROratingViewModels { get; set; }
        public IEnumerable<IRCSignatory> GetRCSignatories { get; set; }



    }
}
