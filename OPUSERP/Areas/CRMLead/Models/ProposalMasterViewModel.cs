using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class ProposalMasterViewModel
    {
        public int proposalMasterId { get; set; }
        public int? proposalTypeId { get; set; }
        public int? leadsId { get; set; }
        public int? proposalStatusId { get; set; }

        public string proposalNo { get; set; }
        public DateTime? proposalDate { get; set; }
        public int?[] productList { get; set; }
        public string productName { get; set; }
       
        public string leadName { get; set; }

        public string[] itemSpecificationName { get; set; }
        public int?[] proposalParticularsId { get; set; }
        public string[] particularsValue { get; set; }

        public string actionName { get; set; }
        public string actionDetails { get; set; }

        public IEnumerable<ProposalMaster> proposalMasters { get; set; }
        public IEnumerable<ProposalType> proposalTypes { get; set; }
        public IEnumerable<Leads> leads { get; set; }
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<RelProductProposal> relProductProposals { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public ProposalMaster GetProposalMasterDetailsById { get; set; }
        public IEnumerable<ProposalParticulars> proposalParticulars { get; set; }
        public IEnumerable<ProposalDetail> proposalDetails { get; set; }
        public IEnumerable<ProposalStatus> proposalStatuses { get; set; }
    }
}
