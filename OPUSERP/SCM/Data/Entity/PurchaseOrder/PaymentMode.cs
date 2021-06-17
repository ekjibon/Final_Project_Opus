using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("PaymentMode", Schema = "SCM")]
    public class PaymentMode:Base
    {
        [Column(TypeName = "nvarchar(200)")]
        public string paymentModeName { get; set; }

        public int? shortOrder { get; set; }
    }
}
