using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Notes;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class CRMNoteMasterViewModel
    {
        public int noteMasterId { get; set; }
       
        public int? leadsId { get; set; }
       public string title { get; set; }
       public string notesdescription { get; set; }
        public string leadName { get; set; }
        public IEnumerable<CRMNoteMaster> cRMNoteMasters { get; set; }
    }
}
