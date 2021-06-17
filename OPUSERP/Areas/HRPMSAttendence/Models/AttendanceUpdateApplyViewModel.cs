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
    public class AttendanceUpdateApplyViewModel
    {
        public int editId { get; set; }
        public int employeeInfoId { get; set; }
        public string applicationNo { get; set; }
        public DateTime? applyDateTime { get; set; }
        public string notes { get; set; }
        public int? approvalStatus { get; set; }

        public int?[] registerids { get; set; }
        public int?[] ids { get; set; }

        public IEnumerable<AttendanceUpdateApply> attendanceUpdateApplies { get; set; }
        public ApprovalDetail approvalDetail { get; set; }
        public IEnumerable<AttendanceRoute> attendanceRoutes { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
