using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("BalanceSheetMaster", Schema = "ACCOUNT")]
    public class BalanceSheetMaster : Base
    {
        public int? accountGroupId { get; set; }
        public AccountGroup accountGroup { get; set; }

        [MaxLength(350)]
        public string fsLineName { get; set; }

        [MaxLength(30)]
        public string noteNo { get; set; }        

        public int? serialNo { get; set; }

        [MaxLength(3)]
        public string fsLineFor { get; set; } //PLA=Profit & Loss Account, BSS=Balance Sheet Statement

    }
}
