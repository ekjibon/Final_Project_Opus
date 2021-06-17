using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class HrBelongingReportViewModel
    {
        public Int64? rowSlNo { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string deptName { get; set; }
        public string emailAddress { get; set; }
        public string mobileNumberOffice { get; set; }
        public string ItemName { get; set; }
    }
}
