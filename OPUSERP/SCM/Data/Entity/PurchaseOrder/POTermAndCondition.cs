using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("POTermAndCondition", Schema = "SCM")]
    public class POTermAndCondition:Base
    {
        public int? purchaseId { get; set; }
        public PurchaseOrderMaster purchase { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string tarmsType { get; set; }

        public string description { get; set; }
    }
}
