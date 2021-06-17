using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLeave.Models
{
    public class LeaveSupervisorRecomViewModel
    {
        public string supervisorName { get; set; }
        public string supervisorDesignation { get; set; }
        public int? leaveStatus { get; set; }
        public DateTime? date { get; set; }
    }
}
