using OPUSERP.Areas.Program.Models;
using OPUSERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
    public interface IProgramReportService
    {
        Task<ProgramReportListModel> programReportListModel(int mainCat, int subcat, DateTime FromDate, DateTime ToDate);
        Task<ProgramDashboardViewModel> GetProgramDashboardViewModel();
    }
}
