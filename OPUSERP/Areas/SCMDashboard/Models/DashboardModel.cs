using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMDashboard.Models
{
    public class DashboardModel
    {

        public IEnumerable<RequisitionMaster> requisitionMasters { get; set; }

        public IEnumerable<RequisitionMaster> approvalStage { get; set; }

        public IEnumerable<PurchaseOrderMaster> purchaseOrderMasters { get; set; }

        public IEnumerable<CSMaster> purchaseOrderStages { get; set; }


    }
}
