using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;

namespace OPUSERP.Production.Data.Entity
{
    [Table("BOMDetails", Schema = "PROD")]
    public class BOMDetail : Base
    {
        public int? bOMMasterId { get; set; }
        public BOMMaster  bOMMaster { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? rate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? wastePercent { get; set; }

    }
}
