using OPUSERP.HRPMS.Data.Entity.ACR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class ACRScheduleViewModel
    {
        public string employeeName { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int? status { get; set; }//1=working,2=pending,4=finished
        public IEnumerable<ACRSchedule> aCRSchedules { get; set; }
    }
}
