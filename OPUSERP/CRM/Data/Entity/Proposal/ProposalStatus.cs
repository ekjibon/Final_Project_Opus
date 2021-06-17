using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Proposal
{
    [Table("ProposalStatus", Schema = "CRM")]
    public class ProposalStatus : Base
    {
        [MaxLength(250)]
        public string proposalStatusName { get; set; }
    }
}
