using OPUSERP.Areas.SCMReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Report.Interface
{
    public interface IReportService
    {
        Task<IEnumerable<NumberViewModel>> GetNumber(int? projectId, int? supplierId, DateTime? fromDate, DateTime? toDate, string type);
    }
}
