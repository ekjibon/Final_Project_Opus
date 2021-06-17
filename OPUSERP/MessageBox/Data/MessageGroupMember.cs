using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.MessageBox.Data
{
    [Table("MessageGroupMember", Schema = "MBox")]
    public class MessageGroupMember:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? messageGroupId { get; set; }
        public MessageGroup messageGroup { get; set; }
    }
}
