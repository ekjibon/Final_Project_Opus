using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramYearViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }

        public IEnumerable<ProgramYear> programYears { get; set; }
    }
}
