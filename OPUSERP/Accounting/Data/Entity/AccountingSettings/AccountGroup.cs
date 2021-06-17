using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("AccountGroup", Schema = "ACCOUNT")]
    public class AccountGroup : Base
    {
        public int? natureId { get; set; }
        public GroupNature nature { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string groupCode { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string groupName { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string groupNameBN { get; set; }
        public int? VGrpTypeCode { get; set; }
        [MaxLength(3)]
        public string isCashBank { get; set; }

        public int? parentGroupId { get; set; }
        public AccountGroup parentGroup { get; set; }

        public DateTime? effectiveDate { get; set; }

    }
}
