using OPUSERP.Data.Entity;
using OPUSERP.Production.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Production.Data.Entity
{
    [Table("ProductionDetails", Schema = "PROD")]
    public class ProductionDetails:Base
    {
        public int? productionId { get; set; }
        public ProductionMaster production { get; set; }

        public int? bOMDetailId { get; set; }
        public BOMDetail bOMDetail { get; set; }

        public decimal? qty { get; set; }

        public decimal? defaultPrice { get; set; }
        public decimal? wastePercent { get; set; }

        public int? defaultSupplierId { get; set; }
    }
}
