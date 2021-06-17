using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramReportListSubModel
    {
        public DateTime? date { get; set; }
        public string place { get; set; }
        public string union { get; set; }
        public string thana { get; set; }
        public string district { get; set; }
        public string number { get; set; }
        public string present { get; set; }
        public string url { get; set; }
    }
}
