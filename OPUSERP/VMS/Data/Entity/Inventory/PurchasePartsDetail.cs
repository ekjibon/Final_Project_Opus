using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Inventory
{
    [Table("PurchasePartsDetail", Schema = "VMS")]
    public class PurchasePartsDetail: Base
    {
        public int? purchasePartsMasterId { get; set; }
        public PurchasePartsMaster purchasePartsMaster { get; set; }
        
        [MaxLength(250)]
        public string barCode { get; set; }       
        public int? isUse { get; set; }
       
        public decimal? quantity { get; set; }        
        public decimal? price { get; set; }
    }
}
