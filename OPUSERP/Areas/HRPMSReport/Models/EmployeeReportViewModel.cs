using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class EmployeeReportViewModel
    {
        public IEnumerable<EmployeeReport> employeeReports { get; set; }

        public List<string> qList { get; set; }

        public EmployeeInfoLn fLang { get; set; }

        public string QueryString { get; set; }
    }
}
