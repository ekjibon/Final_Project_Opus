using OPUSERP.HRPMS.Data.Entity.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLeave.Models
{
    public class LeaveDayViewModel
    {
        public int? leaveDayId { get; set; }
        public string leaveDayName { get; set; }
        public string leaveDayNameBn { get; set; }
        public string description { get; set; }
        public string dayStartTime { get; set; }
        public string dayEndTime { get; set; }

        public IEnumerable<LeaveDay> leaveDays { get; set; }
    }
}
