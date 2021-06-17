using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class HrReportViewModel
    {
        public Int64? rowSlNo { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string deptName { get; set; }
        public string branchUnitName { get; set; }
        public string emailAddress { get; set; }
        public string mobileNumberOffice { get; set; }
        public string bloodGroup { get; set; }
        public string joiningDate { get; set; }
        public string dateOfBirth { get; set; }
        public string nationalID { get; set; }
        public string homeDistrict { get; set; }
        public string lastPromotionDate { get; set; }
        public string activityStatus { get; set; }
    }
}
