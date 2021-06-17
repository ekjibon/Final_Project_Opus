using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("WorkHistory", Schema = "HR")]
    public class WorkHistory:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo emplyeeInfo { get; set; }

        public int teamSize { get; set; }
        public string teamPosition { get; set; }
        public string workType { get; set; }
        public string workDuration { get; set; }
        public string task { get; set; }
    }
}
