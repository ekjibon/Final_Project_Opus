using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Models
{
    public class ProgramDashboardViewModel
    {
        public int? plan { get; set; }
        public int? program { get; set; }
        public decimal? perticipents { get; set; }
        public int? videos { get; set; }
        public IEnumerable<MasterPerticipants> masterPerticipants { get; set; }

    }

    public class MasterPerticipants
    {
        public string name { get; set; }
        public int? count { get; set; }
    }
}
