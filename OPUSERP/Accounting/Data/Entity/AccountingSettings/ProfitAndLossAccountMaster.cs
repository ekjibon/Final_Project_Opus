using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("ProfitAndLossAccountMaster", Schema = "ACCOUNT")]
    public class ProfitAndLossAccountMaster : Base
    {
        public int? accountGroupId { get; set; }
        public AccountGroup accountGroup { get; set; }

        [MaxLength(350)]
        public string noteName { get; set; }

        [MaxLength(30)]
        public string noteNo { get; set; }        

        public int? serialNo { get; set; }       

       
        
    }
}
