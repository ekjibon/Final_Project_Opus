using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class DailyBillReceivePdfVM
    {
        public Company Company { get; set; }
        public IEnumerable<DailyBillReceiveReportViewModel> dailyBillReceiveReportViewModels { get; set; }

    }
}
