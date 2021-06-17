using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingAttendance", Schema = "MMS")]
    public class MeetingAttendance:Base
    {
        public int? meetingInfoId { get; set; }
        public MeetingInfo meetingInfo { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? isAttendance { get; set; }
    }
}
