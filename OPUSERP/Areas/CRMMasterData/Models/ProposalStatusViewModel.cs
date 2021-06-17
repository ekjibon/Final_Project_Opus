using OPUSERP.CRM.Data.Entity.Proposal;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ProposalStatusViewModel
    {
        public int proposalStatusId { get; set; }
        public string proposalStatusName { get; set; }
        public IEnumerable<ProposalStatus> proposalStatuses { get; set; }
    }
}
