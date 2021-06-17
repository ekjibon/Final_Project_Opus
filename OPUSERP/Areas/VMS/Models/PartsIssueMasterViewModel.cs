using System;
using System.Collections.Generic;
using OPUSERP.VMS.Data.Entity.Inventory;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class PartsIssueMasterViewModel
    {
        public int? partsIssueMasterId { get; set; }

        public int? purchasePartsMasterId { get; set; }

        public int? vehicleId { get; set; }

        public string issueNo { get; set; }

        public DateTime? issueDate { get; set; }

        public decimal? quantity { get; set; }

        public int?[] partsDetailId { get; set; }

        //public PartsIssueLN flang { get; set; }

        public PurchasePartsMaster partsMaster { get; set; }

        public IEnumerable<PurchasePartsDetail> purchasePartsDetails { get; set; }

        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }

        public IEnumerable<PartsIssueMaster> partsIssues { get; set; }

    }
}
