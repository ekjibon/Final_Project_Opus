using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("OfficeAssign", Schema = "HR")]
    public class OfficeAssign:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo  { get; set; }

        public string roomNo { get; set; }
        public string floorNo { get; set; }
        public string deskNo { get; set; }
    }
}
