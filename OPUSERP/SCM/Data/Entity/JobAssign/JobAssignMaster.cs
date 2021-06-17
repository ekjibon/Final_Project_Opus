using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.JobAssign
{
    [Table("JobAssignMaster", Schema = "SCM")]
    public class JobAssignMaster:Base
    {
        public int? requisitionId { get; set; }
        public RequisitionMaster requisition { get; set; }

        public int? teamId { get; set; }
        public TeamMaster team { get; set; }

        public DateTime? assignDate { get; set; }

        public int? statusId { get; set; }

        public int? assignTypeId { get; set; }
        public JobAssignType assignType { get; set; }
    }
}
