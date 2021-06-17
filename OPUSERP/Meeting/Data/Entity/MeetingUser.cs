using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingUser", Schema = "MMS")]
    public class MeetingUser:Base
    {
        public string role { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
    }
}
