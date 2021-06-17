using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("PaymentTerms", Schema = "SCM")]
    public class PaymentTerms:Base
    {
        [Column(TypeName = "nvarchar(100)")]
        public string paymentTermsCode { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string paymentTermsName { get; set; }

        public int? shortOrder { get; set; }
    }
}
