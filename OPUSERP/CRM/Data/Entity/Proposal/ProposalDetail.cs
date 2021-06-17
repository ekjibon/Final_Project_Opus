using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Proposal
{
    [Table("ProposalDetail", Schema = "CRM")]
    public class ProposalDetail : Base
    {
        public int? proposalMasterId { get; set; }
        public ProposalMaster proposalMaster { get; set; }

        public int? proposalParticularsId { get; set; }
        public ProposalParticulars proposalParticulars { get; set; }

        public string particularsValue { get; set; }

        [NotMapped]
        public string proposalParticularsName { get; set; }

        [NotMapped]
        public string specificationName { get; set; }
    }
}
