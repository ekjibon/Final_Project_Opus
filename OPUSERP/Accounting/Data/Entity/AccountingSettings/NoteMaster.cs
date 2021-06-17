using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("NoteMaster", Schema = "ACCOUNT")]
    public class NoteMaster : Base
    {
        
      
        [MaxLength(350)]
        public string noteName { get; set; }

        [MaxLength(350)]
        public string noteHead { get; set; }

        public int? nmSerialNo { get; set; }

        public int? balanceSheetMasterId { get; set; }
        public BalanceSheetMaster balanceSheetMaster { get; set; }



    }
}
