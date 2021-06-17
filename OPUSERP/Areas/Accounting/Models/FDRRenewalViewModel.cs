using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class FDRRenewalViewModel
    {
        public IEnumerable<Bank> banks { get; set; }
        public FDRMaturityStatusForRenewalViewModel fDRMaturityStatusForRenewalViewModels { get; set; }
        public IEnumerable<FDRReportViewModel> fDRReportViewModel { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<FDRScheduleReportViewModel> fDRScheduleReportViewModels { get; set; }
        public IEnumerable<FDRInterestScheduleReportViewModel> fDRInterestScheduleReportViewModels { get; set; }
    }
}
