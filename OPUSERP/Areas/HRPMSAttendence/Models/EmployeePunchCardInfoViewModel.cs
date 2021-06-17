using OPUSERP.Areas.HRPMSAttendence.Models.Lang;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Wages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    public class EmployeePunchCardInfoViewModel
    {
        public int editId { get; set; }
        [Required]
        [Display(Name = "PunchCardNo")]
        public string punchCardNo { get; set; }

        public int? shiftGroupMasterId { get; set; }

        public int? employeeId { get; set; }
        
        public string employeeCode { get; set; }
        public DateTime? attendanceProcessDate { get; set; }
        public AttendanceLn fLang { get; set; }

        public IEnumerable<ShiftGroupMaster> shiftGroupMasterlist { get; set; }
        public IEnumerable<Department> departments { get; set; }
        public IEnumerable<EmployeePunchCardInfo> employeePunchCardInfoslist { get; set; }
        public IEnumerable<WagesPunchCardInfo> wagesPunchCardInfos { get; set; }
        public IEnumerable<ShiftGroupMaster> shiftGroupMasterslist { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<EmpAttendanceViewModel> empAttendanceViewModels { get; set; }
        public IEnumerable<JobCardReportViewModel> jobCardReportViewModels { get; set; }
        public IEnumerable<IndividualAttendanceReportViewModel> individualAttendanceReportViewModels { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<IndividualAttendanceSummaryReportViewModel> individualAttendanceSummaryReportViewModels { get; set; }
        public string visualEmpCodeName { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
    }
}
