using OPUSERP.Programs.Data.Entity;
using System.Collections.Generic;

namespace OPUSERP.Areas.Program.Models
{
    public class ProgramWorkPlanViewModel
    {
        public int? programYearId { get; set; }
        public int? masterId { get; set; }

        public int?[] programActivityIdAll { get; set; }
        public decimal?[] firstMonthAll { get; set; }
        public decimal?[] secondMonthAll { get; set; }
        public decimal?[] thirdMonthAll { get; set; }
        public decimal?[] fourthMonthAll { get; set; }
        public decimal?[] fifthMonthAll { get; set; }
        public decimal?[] sixthMonthAll { get; set; }
        public decimal?[] seventhMonthAll { get; set; }
        public decimal?[] eighthMonthAll { get; set; }
        public decimal?[] ninethMonthAll { get; set; }
        public decimal?[] tenthMonthAll { get; set; }
        public decimal?[] eleventhMonthAll { get; set; }
        public decimal?[] twelvethMonthAll { get; set; }
        public decimal?[] subTotalAll { get; set; }

        public string remarks { get; set; }

        public IEnumerable<ProgramWorkPlan> programWorkPlans { get; set; }
        public IEnumerable<ProgramYear> programYears { get; set; }
        public IEnumerable<ProgramMaster> programMasters { get; set; }
        public IEnumerable<ProgramActivity> programActivities { get; set; }
        public IEnumerable<ProgramPlanListViewModel> programPlanListViewModels { get; set; }
    }
}
