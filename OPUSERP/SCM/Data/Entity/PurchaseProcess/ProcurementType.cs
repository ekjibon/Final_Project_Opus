using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseProcess
{
    [Table("ProcurementType", Schema = "SCM")]
    public class ProcurementType:Base
    {
        [Column(TypeName = "nvarchar(100)")]
        public string procurementTypeName { get; set; }

        public int? shortOrder { get; set; }
    }
}
