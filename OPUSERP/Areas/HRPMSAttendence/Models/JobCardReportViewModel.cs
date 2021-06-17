using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity.Auth;

namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    [NotMapped]
    public class JobCardReportViewModel
    {
        
        public Int64? ID { get; set; }
        public string EntryBy { get; set; }
        public string EmpCode { get; set; }
        public string PunchCardNo { get; set; }
        public string Department { get; set; }
        
        public string WorkDate { get; set; }
        public string EmpName { get; set; }
        public string Remarks { get; set; }
       
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public decimal? WorkingTime { get; set; }
        public string Working_Time { get; set; }
        public decimal? latetime { get; set; }
        public string late_time { get; set; }
        public string Status { get; set; }
        public string ShiftName { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
    }
}
