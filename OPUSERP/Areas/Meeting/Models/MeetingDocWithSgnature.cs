using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Meeting.Data.Entity;
using OPUSERP.Workflow.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingDocWithSgnature
    {
        public MeetingDocument doc { get; set; }

        public EmployeeSignature employeeSignature { get; set; }
    }
}
