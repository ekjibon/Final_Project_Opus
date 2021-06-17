using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class HrTrainingReportViewModel
    {
        public Int64? rowSlNo { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string deptName { get; set; }
        public string course { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? days { get; set; }
        public string year { get; set; }
        public string completionStatus { get; set; }
    }
}
