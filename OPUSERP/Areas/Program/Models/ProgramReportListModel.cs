using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramReportListModel
    {
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public IEnumerable<ProgramReportListSubModel> programReportListSubModels { get; set; }
    }
}
