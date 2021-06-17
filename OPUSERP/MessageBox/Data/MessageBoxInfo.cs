using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.MessageBox.Data
{
    [Table("MessageBoxInfo", Schema = "MBox")]
    public class MessageBoxInfo:Base
    {
        public string message { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? messageBoxId { get; set; }
        public MessageBoxInfo messageBox { get; set; }

        public int? messageGroupId { get; set; }
        public MessageGroup messageGroup { get; set; }

        public int? receiverId { get; set; }
        public EmployeeInfo receiver { get; set; }

        public DateTime? date { get; set; }
    }
}
