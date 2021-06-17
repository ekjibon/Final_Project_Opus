using OPUSERP.Data.Entity.Master;
using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramPlanViewModel
    {
        public int Id { get; set; }

        public string number { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public int? programCategoryId { get; set; }

        public int? programYearId { get; set; }

        public int? districtId { get; set; }

        public int? divisionId { get; set; }

        public IEnumerable<ProgramMainCategory> programMainCategories { get; set; }
        public IEnumerable<programPlan> programPlans { get; set; }
        public IEnumerable<ProgramYear> programYears { get; set; }
        public IEnumerable<Division> divisions { get; set; }
    }
}
