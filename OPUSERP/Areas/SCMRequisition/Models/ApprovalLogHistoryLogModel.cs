using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
    public class ApprovalLogHistoryLogModel
    {
        public int? Id { get; set; }
        public string nameEnglish { get; set; }
        public string designationName { get; set; }
        public int? sequenceNo { get; set; }
        public int? isActive { get; set; }
        public string url { get; set; }
        public string approvedDate { get; set; }
    }
}
