using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLeave.Models.NotMapped
{
    public class LeaveSummary
    {
        public string type { get; set; }
        public int Opening { get; set; }
        public int Leave { get; set; }
        public int Balance { get; set; }
    }
}
