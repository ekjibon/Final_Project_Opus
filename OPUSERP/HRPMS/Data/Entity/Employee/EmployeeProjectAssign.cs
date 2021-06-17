using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("EmployeeProjectAssign", Schema = "HR")]
    public class EmployeeProjectAssign : Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? projectId { get; set; }
        public Project project { get; set; }
        
        public int? isActive { get; set; }
    }
}
