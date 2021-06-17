using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAttendence.Models.Lang
{
    public class AttendanceLn
    {
        public string title { get; set; }
        public string employeeCode { get; set; }
        public string shiftGroup { get; set; }
        public string punchCardNo { get; set; }
        public string attendanceProcessDate { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string action { get; set; }
    }
}
