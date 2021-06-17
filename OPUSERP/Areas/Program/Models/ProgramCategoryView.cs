using OPUSERP.Data.Entity;
using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramCategoryView
    {
        public int? programMainCategoryId { get; set; }

        public int programCategoryId { get; set; }

        public string name { get; set; }


        public IEnumerable<ProgramMainCategory> programMainCategorys { get; set; }
        public IEnumerable<ProgramCategory> programCategorys { get; set; }

    }
}
