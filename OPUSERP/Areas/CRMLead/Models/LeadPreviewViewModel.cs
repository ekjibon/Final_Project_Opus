using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.Master;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class LeadPreviewViewModel
    {
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Contacts> contacts { get; set; }
        public IEnumerable<ActivityMasterCViewModel> activityMasterCViewModels { get; set; }
        public IEnumerable<ProposalMaster> proposalMasters { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
        public IEnumerable<Agreement> agreements { get; set; }
        public IEnumerable<LeadsHistory> leadsHistories { get; set; }
        public Leads leads { get; set; }
    }
}
