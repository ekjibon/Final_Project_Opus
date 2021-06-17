using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Production.Data.Entity
{
    [Table("BOMOverheadDetail", Schema = "PROD")]
    public class BOMOverheadDetail : Base
    {
        public int? bOMMasterId { get; set; }
        public BOMMaster  bOMMaster { get; set; }
        public int? overheadCostId { get; set; }
        public OverheadCost overheadCost  { get; set; }
        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? cost { get; set; }
        public string costType { get; set; }

    }
}
