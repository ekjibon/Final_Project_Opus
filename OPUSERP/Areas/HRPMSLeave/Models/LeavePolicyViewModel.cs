using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.Areas.HRPMSLeave.Models
{
    public class LeavePolicyViewModel
    {
        [Display(Name = "Leave Type")]
        public int? leaveTypeId { get; set; }

        public int id { get; set; }
        public int? employeeId { get; set; }
        
        [Display(Name = "Year")]
        public int? year { get; set; }
        
        [Display(Name = "Yearly Max Leave")]
        public int? maxYearlyLeave { get; set; }
        
        [Display(Name = "Max carry Leave")]
        public int? maxCarry { get; set; }
        
        [Display(Name = "Weekly Bridge")]
        public int? weeklyBridge { get; set; }
        
        [Display(Name = "Govt. Holiday Bridge")]
        public int? govtHolidayBridge { get; set; }

        public string remarks { get; set; }
        public string paymentType { get; set; }

        public int? maxBridgeLimit { get; set; }
        public int? highestCarryForward { get; set; }

        public IEnumerable<LeaveType> leaveTypelist { get; set; }
        public IEnumerable<Year> years { get; set; }
        public IEnumerable<LeavePolicy> leavePolicies { get; set; }
        public IEnumerable<LeaveOpeningBalance> leaveOpeningBalances { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
