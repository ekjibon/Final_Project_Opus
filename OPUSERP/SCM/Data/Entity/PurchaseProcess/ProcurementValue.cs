using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseProcess
{
    [Table("ProcurementValue", Schema = "SCM")]
    public class ProcurementValue:Base
    {
        [Column(TypeName = "nvarchar(100)")]
        public string procurementValueName { get; set; }

        public int? shortOrder { get; set; }
    }
}
