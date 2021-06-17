using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class LeadsHistoryViewModel
    {
        public int? leadsId { get; set; }
        public string actionName { get; set; }
        public string actionDetails { get; set; }
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }

        public IEnumerable<LeadsHistory> leadsHistories { get; set; }
    }
}
