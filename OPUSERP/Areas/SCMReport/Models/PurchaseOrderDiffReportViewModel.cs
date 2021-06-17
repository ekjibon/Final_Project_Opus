using OPUSERP.Areas.SCMIOU.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMReport.Models
{
    public class PurchaseOrderDiffReportViewModel
    {
       
        public string poNo { get; set; }
        public DateTime? poDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? reqDate { get; set; }
        public string organizationName { get; set; }
      
     
        public int? dayDiff { get; set; }
      

    }
}
