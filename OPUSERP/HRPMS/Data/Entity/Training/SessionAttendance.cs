using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("SessionAttendance", Schema = "HR")]
    public class SessionAttendance:Base
    {
        public int? trainingInfoDetailsId { get; set; }
        public TrainingInfoDetails trainingInfoDetails { get; set; }

        //Foreign Relation->Resource
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string status { get; set; } // Present Or Absent
    }
}
