using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRO.Models
{
    public class LeadInCroViewModel
    {
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Contacts> contacts { get; set; }
        public IEnumerable<ActivityMasterCViewModel> activityMasterCViewModels { get; set; }
        public IEnumerable<ProposalMaster> proposalMasters { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
        public IEnumerable<Agreement> agreements { get; set; }
        public IEnumerable<LeadsHistory> leadsHistories { get; set; }
        public IEnumerable<GetLeadInfoInCroViewModel> getLeadInfoInCroViewModels { get; set; }
        public IEnumerable<StatusLog> statusLogs { get; set; }
        public IEnumerable<ReceiveDocument> receiveDocuments { get; set; }
        public IEnumerable<CroAttachmentViewModel> croAttachmentViewModels { get; set; }
        public IEnumerable<IRCMeetingAttendance> iRCMeetingAttendances { get; set; }
        public IEnumerable<IRCMemberComments> iRCMemberComments { get; set; }
        public Leads leads { get; set; }
        public ProposedRating proposedRating { get; set; }
        public IEnumerable<IRCRating> iRCRating { get; set; }
    }
}
