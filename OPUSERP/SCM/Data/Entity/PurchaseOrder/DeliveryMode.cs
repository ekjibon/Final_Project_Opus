using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("DeliveryMode", Schema = "SCM")]
    public class DeliveryMode:Base
    {
        [Column(TypeName = "nvarchar(200)")]
        public string deliveryModeName { get; set; }

        public int? shortOrder { get; set; }
    }
}
