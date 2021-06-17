using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMJobAssign.Models
{
    public class JobAssignViewModel
    {
        public int?[] masterIds { get; set; }
        public int?[] teamIds { get; set; }

        public int rBuyer { get; set; }

        public int? singleMemberIds { get; set; }
        public int?[] MemberIds { get; set; }
        public int?[] reqDetailIds { get; set; }

        public IEnumerable<RequisitionMaster> requisitionMasters { get; set; }
        public IEnumerable<RequisitionMaster> assignRequisitionMasters { get; set; }
        public IEnumerable<TeamMaster> teamMasters { get; set; }
    }
}
