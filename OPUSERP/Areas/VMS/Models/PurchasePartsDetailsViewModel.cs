using System.Collections.Generic;

using OPUSERP.VMS.Data.Entity.Inventory;

namespace OPUSERP.Areas.VMS.Models
{
    public class PurchasePartsDetailsViewModel
    {
        public int? purchasePartsMasterId { get; set; }

        public string barCode { get; set; }

        public int? isUse { get; set; }

        public decimal? quantity { get; set; }

        public decimal? price { get; set; }

        //public PurchasePartsDetailsLN flang { get; set; }
        public IEnumerable<PurchasePartsDetail> purchasePartsDetails { get; set; }
    }
}
