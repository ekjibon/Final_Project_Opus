using OPUSERP.Areas.SCMRequisition.Models.Lang;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
    public class GetRequisitionListForApprovedViewModel
    {
        public int Id { get; set; }
      
        public int? isDelete { get; set; }

        public DateTime? createdAt { get; set; }

        public DateTime? updatedAt { get; set; }
       
        public string createdBy { get; set; }
      
        public string updatedBy { get; set; }
        public string reqNo { get; set; }
        public DateTime? reqDate { get; set; }

        public DateTime? targetDate { get; set; }

        public string justification { get; set; }

        public string subject { get; set; }

        public int? reqBy { get; set; }
       
        public string reqDept { get; set; }

        public int? projectId { get; set; }
        public string reqType { get; set; }

        public int? teamMasterId { get; set; }
        public DateTime? assignDate { get; set; }

        public DateTime? approveDate { get; set; }

       
        public string isPostPR { get; set; }

        public int isActive { get; set; }

        public int? statusInfoId { get; set; }
        public string projectName { get; set; }
    }
    
}
