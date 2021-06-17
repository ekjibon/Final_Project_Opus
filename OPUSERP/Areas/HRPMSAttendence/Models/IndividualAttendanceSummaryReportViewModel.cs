using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity.Auth;

namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    [NotMapped]
    public class IndividualAttendanceSummaryReportViewModel
    {
       
        public string EmpCode { get; set; }
        public string PunchCardNo { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string EmpName { get; set; }
        public Int32? Late { get; set; }
        public Int32? Holidays { get; set; }
        public Int32? present { get; set; }
        public Int32? Absent { get; set; }
        public Int32? WeekilyOFF { get; set; }
        public Int32? Tdays { get; set; }
        public Int32? DaysLeave { get; set; }
    }
}
