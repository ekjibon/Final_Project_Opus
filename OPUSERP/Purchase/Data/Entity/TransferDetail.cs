using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Purchase.Data.Entity
{
    [Table("TransferDetail", Schema = "Purchase")]
    public class TransferDetail : Base
    {
        public int? transferMasterId { get; set; }
        public TransferMaster transferMaster { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? qty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? rate { get; set; }

        [NotMapped]
        public string specName { get; set; }

    }
}
