using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("OtherQualification", Schema = "HR")]
    public class OtherQualification:Base
    {
        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? otherQualificationHeadId { get; set; }
        public OtherQualificationHead otherQualificationHead { get; set; }

        public string subject { get; set; }

        public int? resultId { get; set; }
        public Result result { get; set; }

        public string instituteName { get; set; }

        public string passingYear { get; set; }

        public string markGrade { get; set; }
    }
}
