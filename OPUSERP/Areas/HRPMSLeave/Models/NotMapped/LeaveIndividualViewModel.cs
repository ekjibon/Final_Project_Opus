using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLeave.Models.NotMapped
{
    public class LeaveIndividualViewModel
    {
        public DateTime? LF { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string deptName { get; set; }
        public string designation { get; set; }
        public string joiningDate { get; set; }
        public string leaveFrom { get; set; }
        public string leaveTo { get; set; }
        public int? daysLeave { get; set; }
        public string purposeOfLeave { get; set; }
        public string leaveTypeName { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
}
