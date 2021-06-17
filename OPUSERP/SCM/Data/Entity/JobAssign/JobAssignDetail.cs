using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.JobAssign
{
    [Table("JobAssignDetail", Schema = "SCM")]
    public class JobAssignDetail:Base
    {
        public int? jobAssignId { get; set; }
        public JobAssignMaster jobAssign { get; set; }

        public int? requisitionDetailId { get; set; }

        public int? teamMemberId { get; set; }
        public TeamMember teamMember { get; set; }

        public DateTime? assignDate { get; set; }
    }
}
