using OPUSERP.SCM.Data.Entity.Requisition;
using System;

namespace OPUSERP.Areas.SCMDashboard.Models
{
    public class DashboardVM
    {
        public int? totalCount { get; set; }
        public decimal? totalValue { get; set; }
        public string itemName { get; set; }
        public DateTime? prDate { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }
    }
}
