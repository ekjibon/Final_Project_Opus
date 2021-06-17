using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class HrSummaryReportViewModel
    {
        public Int64? rowSlNo { get; set; }
        public string deptName { get; set; }
        public int? totalNumber { get; set; }
        public decimal? ratio { get; set; }
    }
}
