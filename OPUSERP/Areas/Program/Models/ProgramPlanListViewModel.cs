using System;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramPlanListViewModel
    {
        public int Id { get; set; }
        public int workPlanId { get; set; }
        public int programYearId { get; set; }
        public string yearName { get; set; }
        public string number { get; set; }

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string projectName { get; set; }
    }
}
