using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.Shagotom.Data.Entity.Visitor
{
    [Table("VisitorEntryMaster", Schema = "Shagotom")]
    public class VisitorEntryMaster : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

       public int? status { get; set; }
    }
}
