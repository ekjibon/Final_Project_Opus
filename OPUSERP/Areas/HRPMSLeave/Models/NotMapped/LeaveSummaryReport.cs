using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLeave.Models.NotMapped
{
    public class LeaveSummaryReport
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<LeaveSummary> leaveSummaries { get; set; }
    }
}
