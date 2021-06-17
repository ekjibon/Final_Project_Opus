using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("Trainer", Schema = "HR")]
    public class Trainer : Base
    {
        //Foreign Relation -> Resource
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string type { get; set; }
    }
}
