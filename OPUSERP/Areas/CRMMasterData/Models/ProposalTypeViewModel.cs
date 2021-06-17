using OPUSERP.CRM.Data.Entity.Proposal;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ProposalTypeViewModel
    {
        public int proposalTypeId { get; set; }
        public string proposalTypeName { get; set; }
        public IEnumerable<ProposalType> proposalTypes { get; set; }
    }
}
