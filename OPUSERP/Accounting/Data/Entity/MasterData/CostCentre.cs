using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.MasterData
{
    [Table("CostCentre", Schema = "ACCOUNT")]
    public class CostCentre : Base
    {
        [Column(TypeName = "nvarchar(250)")]
        public string centreName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string aliesName { get; set; }
        [MaxLength(100)]
        public string centreCode { get; set; }
        public int? companyId { get; set; }

        public int? departmentId { get; set; }
    }
}
