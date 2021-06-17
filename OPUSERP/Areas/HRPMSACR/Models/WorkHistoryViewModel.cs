using OPUSERP.HRPMS.Data.Entity.ACR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class WorkHistoryViewModel
    {
        public string employeeName { get; set; }
        public int teamSize { get; set; }
        public string teamPosition { get; set; }
        public string workType { get; set; }
        public string workDuration { get; set; }
        public string task { get; set; }
        public IEnumerable<WorkHistory> workHistories { get; set; }
    }
}
