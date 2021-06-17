using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Proposal
{
    [Table("RelProductProposal", Schema = "CRM")]
    public class RelProductProposal : Base
    {
        public int? productId { get; set; }
        public Product product { get; set; }

        public int? proposalMasterId { get; set; }
        public ProposalMaster proposalMaster { get; set; }

        [NotMapped]
        public string productName { get; set; }
        [NotMapped]
        public string productDescription { get; set; }
    }
}
