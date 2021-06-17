using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramVideoViewModel
    {
        public IEnumerable<ProgramVideo> programViews { get; set; }
        public ProgramVideo programVideo { get; set; }
    }
}
