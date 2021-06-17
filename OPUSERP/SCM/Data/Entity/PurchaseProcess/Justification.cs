using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseProcess
{
    [Table("Justification", Schema = "SCM")]
    public class Justification:Base
    {
        public int? cSMasterId { get; set; }
        public CSMaster cSMaster { get; set; }

        public int? justificationTypeId { get; set; }
        public JustificationType justificationType { get; set; }

        public int? isJustify { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string justificationValue { get; set; }
    }
}
