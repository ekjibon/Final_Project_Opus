using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("QualificationAndValidation", Schema = "HR")]
    public class QualificationAndValidation:Base
    {
        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? qualificationHeadId { get; set; }
        public QualificationHead qualificationHead { get; set; }

        public string attachment { get; set; }

        public int? isValid { get; set; }
    }
}
