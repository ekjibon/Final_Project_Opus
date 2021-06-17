using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("GroupNature", Schema = "ACCOUNT")]
    public class GroupNature : Base
    {
        [Column(TypeName = "nvarchar(250)")]
        public string natureName { get; set; }

        [Column(TypeName = "nvarchar(350)")]
        public string natureNameBN { get; set; }

        public int? isActive { get; set; }

        public int? printOrder { get; set; }
    }
}
