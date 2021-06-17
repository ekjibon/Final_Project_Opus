using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMDashboard.Models
{
    public class DashboardViewModel
    {
        public DashboardModel dashboardModels { get; set; }
        public IEnumerable<DashboardVM> dashboardVMs { get; set; }
    }
}
