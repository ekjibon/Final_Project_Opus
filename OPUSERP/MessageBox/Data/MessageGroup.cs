using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.MessageBox.Data
{
    [Table("MessageGroup", Schema = "MBox")]
    public class MessageGroup:Base
    {
        public string name { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string tagline { get; set; }

        public string praivacy { get; set; }
    }
}
