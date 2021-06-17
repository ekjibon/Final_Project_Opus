using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("BalanceSheetDetails", Schema = "ACCOUNT")]
    public class BalanceSheetDetails : Base
    {
        public int? noteMasterId { get; set; }
        public NoteMaster noteMaster { get; set; }

        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }
    

       
        
    }
}
