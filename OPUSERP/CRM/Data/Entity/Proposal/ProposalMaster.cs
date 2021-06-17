using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Proposal
{
    [Table("ProposalMaster", Schema = "CRM")]
    public class ProposalMaster : Base
    {
        public int? proposalTypeId { get; set; }
        public ProposalType proposalType { get; set; }        

        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? proposalStatusId { get; set; }
        public ProposalStatus proposalStatus { get; set; }

        [MaxLength(100)]
        public string proposalNo { get; set; }

        public DateTime? proposalDate { get; set; }       

    }
}
