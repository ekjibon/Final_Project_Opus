using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("DeliveryLocation", Schema = "SCM")]
    public class DeliveryLocation:Base
    {
        [Column(TypeName = "nvarchar(250)")]
        public string locationName { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string address { get; set; }

        public int? shortOrder { get; set; }
    }
}
