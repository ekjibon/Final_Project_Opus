using OPUSERP.Areas.HRPMSLeave.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Leave;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLeave.Models
{
    public class LeaveApprovalViewModel
    {
        public int?[] ids { get; set; }
        public int? id { get; set; }
        public int? employeeId { get; set; }
        public int? leaveId { get; set; }
        public int?[] registerids { get; set; }
        public string leaveApprove { get; set; }
        

        public IEnumerable<LeaveRegister> leaveRegisterApprove { get; set; }

        public LeaveRegister LeaveRegister { get; set; }        
    }
}
