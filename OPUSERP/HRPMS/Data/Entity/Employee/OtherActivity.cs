using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("OtherActivity", Schema = "HR")]
    public class OtherActivity:Base
    {
        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? activityNameId { get; set; }
        public ActivityName activityName { get; set; }

        public string name { get; set; }

        public string description { get; set; }
    }
}
