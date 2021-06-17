using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Production.Data.Entity
{
    [Table("OverheadCosts", Schema = "PROD")]
    public class OverheadCost:Base
    {
        [MaxLength(100)]
        public string accountCode { get; set; }
        [MaxLength(300)]
        public string accountName { get; set; }
        public int? haveLedgerRelation { get; set; }
        public int? statusId { get; set; }
        
    }
}
