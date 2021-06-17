using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseProcess
{
    [Table("JustificationType", Schema = "SCM")]
    public class JustificationType:Base
    {
        [Column(TypeName = "nvarchar(200)")]
        public string justificationTypeName { get; set; }

        public int? defaultValue { get; set; }

        public int? shortOrder { get; set; }
    }
}
