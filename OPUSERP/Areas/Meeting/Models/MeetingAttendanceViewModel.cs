using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingAttendanceViewModel
    {
        public int? meetingId { get; set; }
        public int?[] checkbox { get; set; }
    }
}
