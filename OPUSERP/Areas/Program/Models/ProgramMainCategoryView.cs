using OPUSERP.Data.Entity;
using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramMainCategoryView
    {
        public string name { get; set; }
        public int mainProgramCategoryId { get; set; }

        public IEnumerable<ProgramMainCategory> programMainCategories { get; set; }
    }
}
