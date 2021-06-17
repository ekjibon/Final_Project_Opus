using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.CarManagement;

namespace OPUSERP.VMS.Data.Entity.Inventory
{
    [Table("PurchasePartsMaster", Schema = "VMS")]
    public class PurchasePartsMaster: Base
    {
        public int? sparePartsId { get; set; }
        public SpareParts spareParts { get; set; }

        public DateTime? purchaseDate { get; set; }
        [MaxLength(250)]
        public string purchaseBy { get; set; }
        [MaxLength(100)]
        public string challanNo { get; set; }
        
        public decimal? quantity { get; set; }        
        public decimal? price { get; set; }
    }
}
