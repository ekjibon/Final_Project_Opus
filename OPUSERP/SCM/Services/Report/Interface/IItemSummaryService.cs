using OPUSERP.Areas.SCMReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Report.Interface
{
    public interface IItemSummaryService
    {
        Task<IEnumerable<ItemSummaryViewModel>> GetItemSummary(DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<ItemSummaryViewModel>> ItemwiseIssueDetails(DateTime? fromDate, DateTime? toDate);
    }
}
