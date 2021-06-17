using OPUSERP.CRM.Data.Entity.Proposal;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ProposalParticularsViewModel
    {
        public int proposalParticularsId { get; set; }
        public string proposalParticularsName { get; set; }
        public IEnumerable<ProposalParticulars> proposalParticulars { get; set; }
    }
}
