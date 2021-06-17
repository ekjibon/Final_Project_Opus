using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Data.Entity
{
    [Table("ProductionRequsitionDetails", Schema = "PROD")]
    public class ProductionRequsitionDetails:Base
    {
        public int? productionRequsitionMasterId { get; set; }
        public ProductionRequsitionMaster productionRequsitionMaster { get; set; }
        public int? BOMDetailId { get; set; }
        public BOMDetail BOMDetail { get; set; }
        public decimal? requsitionQuantity { get; set; }
        public decimal? rate { get; set; }
    }
}
