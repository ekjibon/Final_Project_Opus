using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingDocRoute", Schema = "MMS")]
    public class MeetingDocRoute:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? employeeReplaceId { get; set; }
        public EmployeeInfo employeeReplace { get; set; }

        public int? docId { get; set; }
        public MeetingDocument doc { get; set; }

        public int? actionId { get; set; }
        public MeetingActionInfo action { get; set; }

        public int? order { get; set; }

        public int? isActive { get; set; }

        public string status { get; set; }

        public string remarks { get; set; }
    }
}
