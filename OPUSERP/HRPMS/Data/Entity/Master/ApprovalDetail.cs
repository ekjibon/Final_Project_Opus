using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("ApprovalDetail", Schema = "HR")]
    public class ApprovalDetail : Base
    {
        public int? approvalMasterId { get; set; }
        public ApprovalMaster  approvalMaster { get; set; }

        public int? approverId { get; set; }
        public EmployeeInfo approver { get; set; }

        public int? sortOrder { get; set; }
        public string status { get; set; }
    }
}
