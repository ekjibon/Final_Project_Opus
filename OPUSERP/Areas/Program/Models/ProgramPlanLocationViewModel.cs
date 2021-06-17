using OPUSERP.Programs.Data.Entity;
using System.Collections.Generic;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramPlanLocationViewModel
    {
        public int editId { get; set; }

        public int? programActivityId { get; set; }
        public int? clickprogramYearId { get; set; }
        public string clickMonthName { get; set; }
        public decimal? planQuantity { get; set; }
        public decimal? targetQuantity { get; set; }
        public decimal? executionQuantity { get; set; }

        public int? divisionId { get; set; }
        public int? districtId { get; set; }
        public int? thanaId { get; set; }
        public string maillingAddress { get; set; }
        
    }
}
