using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSReport.Models
{
    public class AllHrReportViewModel
    {
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<HrReportViewModel> hrReportViewModels { get; set; }
        public IEnumerable<HrEducationReportViewModel> hrEducationReportViewModels { get; set; }
        public IEnumerable<HrTrainingReportViewModel> hrTrainingReportViewModels { get; set; }
        public IEnumerable<HrBelongingReportViewModel> hrBelongingReportViewModels { get; set; }
        public IEnumerable<HrSummaryReportViewModel> hrSummaryReportViewModels { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<Subject> subjects { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<LevelofEducation> levelofEducations { get; set; }
        public IEnumerable<CourseTitle> courseTitles { get; set; }
        public IEnumerable<BelongingItem> belongingItems { get; set; }
    }
}
