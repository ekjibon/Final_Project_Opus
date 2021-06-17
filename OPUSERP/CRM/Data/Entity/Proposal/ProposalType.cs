using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Proposal
{
    [Table("ProposalType", Schema = "CRM")]
    public class ProposalType : Base
    {
        [MaxLength(250)]
        public string proposalTypeName { get; set; }
    }
}
