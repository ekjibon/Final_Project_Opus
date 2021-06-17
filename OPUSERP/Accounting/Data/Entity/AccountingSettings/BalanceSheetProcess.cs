using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("BalanceSheetProcess", Schema = "ACCOUNT")]
    public class BalanceSheetProcess : Base
    {
        public int? balanceSheetDetailsId { get; set; }
        public BalanceSheetDetails balanceSheetDetails { get; set; }

        public decimal? currentYearAmount { get; set; }

        public decimal? previousYearAmount { get; set; }

        public int? currentYear { get; set; }
        public int? previousYear { get; set; }

        public int? isLocked { get; set; } //0=unlocked, 1=locked
    }
}
