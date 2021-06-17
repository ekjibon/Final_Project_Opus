using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("ProfitAndLossAccountDetails", Schema = "ACCOUNT")]
    public class ProfitAndLossAccountDetails : Base
    {
        public int? profitAndLossAccountMasterId { get; set; }
        public ProfitAndLossAccountMaster profitAndLossAccountMaster { get; set; }

        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }
    

       
        
    }
}
