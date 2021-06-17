using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("ProfitAndLossAccountProcess", Schema = "ACCOUNT")]
    public class ProfitAndLossAccountProcess : Base
    {
        public int? profitAndLossAccountDetailsId { get; set; }
        public ProfitAndLossAccountDetails profitAndLossAccountDetails { get; set; }

        public decimal? currentYearAmount { get; set; }

        public decimal? previousYearAmount { get; set; }

        public int? currentYear { get; set; }
        public int? previousYear { get; set; }

        public int? isLocked { get; set; } //0=unlocked, 1=locked
    }
}
