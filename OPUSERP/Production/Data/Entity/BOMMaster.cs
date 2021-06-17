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
    [Table("BOMMasters", Schema = "PROD")]
    public class BOMMaster : Base
    {
        public string bomNo { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public string bomItemDescription { get; set; }
        public string operationType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? quantity { get; set; }
        public int? isactive { get; set; }

        public DateTime? bomDate { get; set; }
    }
}
