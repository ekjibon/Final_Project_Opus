using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingDetail", Schema = "MMS")]
    public class MeetingDetail:Base
    {
        public int? meetingInfoId { get; set; }
        public MeetingInfo meetingInfo { get; set; }

        public int? docId { get; set; }
        public MeetingDocument doc { get; set; }

        public int? actionId { get; set; }
        public MeetingActionInfo action { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
    }
}
