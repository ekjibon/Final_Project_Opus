using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("Ledger", Schema = "ACCOUNT")]
    public class Ledger : Base
    {
        public int? groupId { get; set; }
        public virtual AccountGroup group { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string accountCode { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string accountName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string aliasName { get; set; }

        public int? currencyId { get; set; }
        public virtual Currency currency { get; set; }

        public int? haveSubLedger { get; set; }

        public int? companyId { get; set; }

        public int? isActive { get; set; }
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }

        [MaxLength(30)]
        public string ledgerType { get; set; }
        public DateTime? effectiveDate { get; set; }
    }
}
