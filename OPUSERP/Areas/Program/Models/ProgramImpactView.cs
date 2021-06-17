using OPUSERP.Data.Entity;
using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramImpactView
    {
        public int? programImpactId { get; set; }
        public int? programStatusId { get; set; }
        public string  name { get; set; }

        public string  description { get; set; }

       
        public IEnumerable<ProgramImpact> programImpacts { get; set; }
        public IEnumerable<ProgramStatus> programStatuses { get; set; }

    }
}
