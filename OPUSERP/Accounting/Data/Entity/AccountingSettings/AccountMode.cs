using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("AccountMode", Schema = "ACCOUNT")]
    public class AccountMode : Base
    {
        [Column(TypeName = "nvarchar(150)")]
        public string modeName { get; set; }

        public int? shortOrder { get; set; }
    }
}
