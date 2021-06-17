using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("WagesEmergencyContact", Schema = "HR")]
    public class WagesEmergencyContact:Base
    {
        public int employeeID { get; set; }
        public WagesEmployeeInfo employee { get; set; }

        public string name { get; set; }
        public string relation { get; set; }
        public string designation { get; set; }
        public string organization { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
    }
}
