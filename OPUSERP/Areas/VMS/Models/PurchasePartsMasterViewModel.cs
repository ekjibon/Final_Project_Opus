using System;
using System.Collections.Generic;

using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.Inventory;

namespace OPUSERP.Areas.VMS.Models
{
    public class PurchasePartsMasterViewModel
    {
        public int? purchasePartsMasterId { get; set; }

        public int? sparePartsId { get; set; }

        public DateTime? purchaseDate { get; set; }
       
        public string purchaseBy { get; set; }
        
        public string challanNo { get; set; }

        public decimal? quantity { get; set; }

        public decimal? price { get; set; }

        //public PurchasePartsMasterLN flang { get; set; }

        public IEnumerable<PurchasePartsMaster> purchasePartsMasters { get; set; }

        public IEnumerable<SpareParts> spareParts { get; set; }
    }
}
