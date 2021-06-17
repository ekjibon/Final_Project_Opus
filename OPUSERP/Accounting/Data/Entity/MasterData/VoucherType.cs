using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.MasterData
{
    [Table("VoucherType", Schema = "ACCOUNT")]
    public class VoucherType : Base
    {
        [MaxLength(100)]
        public string voucherTypeName { get; set; }
        [MaxLength(100)]
        public string voucherTypeNameBN { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }
        [MaxLength(100)]
        public string abbreviation { get; set; }

        public int isActive { get; set; }
    }
}
