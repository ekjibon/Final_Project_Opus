using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Proposal
{
    [Table("ProposalParticulars", Schema = "CRM")]
    public class ProposalParticulars : Base
    {
        [MaxLength(250)]
        public string proposalParticularsName { get; set; }
    }
}
