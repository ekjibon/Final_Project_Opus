using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.Areas.HRPMSLeave.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.HRPMSLeave.Models.NotMapped;
using OPUSERP.Data.Entity.Master;

namespace OPUSERP.Areas.HRPMSLeave.Models
{
    public class LeaveRegisterViewModel
    {
       public string userName { get; set; }
        public int id { get; set; }
        
        public int[] registerids { get; set; }

        public int? employeeId { get; set; }
        public int? substitutionUserId { get; set; }
        public int? leaveBalance { get; set; }
        public int leaveTypeId { get; set; }
        public int? leaveDayId { get; set; }
        public string whenLeave { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? leaveFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? leaveTo { get; set; }

        public int daysLeave { get; set; }
        public string purposeOfLeave { get; set; }
        public string substitutionEmpCode { get; set; }
        public string emergencyContactNo { get; set; }
        public string leaveStatus { get; set; }
        public string address { get; set; }
        public string txtRemarks { get; set; }
        public string paymentType { get; set; }

        public IFormFile fileUrl { get; set; }

        public LeaveLn fLang { get; set; }

        public LeaveRegister leaveRegister { get; set; }
        public IEnumerable<LeaveRegister> leaveRegisterslist { get; set; }
        public IEnumerable<WagesLeaveRegister> wagesLeaveRegisters { get; set; }
        public IEnumerable<LeaveType> leaveTypelist { get; set; }
        public IEnumerable<LeaveRoute> leaveRoutes { get; set; }
        public IEnumerable<LeaveReportModel> leaveReportModels { get; set; }
        public IEnumerable<Year> years { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<LeaveSummaryReport> leaveSummaryReports { get; set; }
        public IEnumerable<LeaveSupervisorRecomViewModel> leaveSupervisorRecomViewModels { get; set; }
        public IEnumerable<LeaveIndividualViewModel> leaveIndividualViewModels { get; set; }
        public Supervisor supervisor { get; set; }
        public ApprovalDetail approvalDetail { get; set; }
        public IEnumerable<LeaveDay> leaveDays { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public string visualEmpCodeName { get; set; }
    }
}
