using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Data.Entity
{
    [Table("TaskComment", Schema = "TMS")]
    public class TaskComment:Base
    {
        public int? taskInformationId { get; set; }
        public TaskInformation taskInformation { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string comments { get; set; }
    }
}
