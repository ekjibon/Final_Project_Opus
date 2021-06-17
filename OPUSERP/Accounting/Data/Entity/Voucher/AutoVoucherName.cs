using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("AutoVoucherName", Schema = "ACCOUNT")]
    public class AutoVoucherName : Base
    {
        [MaxLength(200)]
        public string autoVoucherName { get; set; }
        [MaxLength(20)]
        public string shortName { get; set; }

    }
}
