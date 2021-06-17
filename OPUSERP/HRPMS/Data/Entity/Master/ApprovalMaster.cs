using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("ApprovalMaster", Schema = "HR")]
    public class ApprovalMaster : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? approvalTypeId { get; set; }
        public ApprovalType approvalType { get; set; }
    }
}
