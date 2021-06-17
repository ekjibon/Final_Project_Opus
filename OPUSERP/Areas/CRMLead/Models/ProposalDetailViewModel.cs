using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class ProposalDetailViewModel
    {
        public int? Id { get; set; }
        public string specificationName { get; set; }
        public string proposalParticularsName { get; set; }
        public int? proposalParticularsId { get; set; }
        public string particularsValue { get; set; }

       
    }
}
