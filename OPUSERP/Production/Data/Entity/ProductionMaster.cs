using OPUSERP.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;
using OPUSERP.Production.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.Production.Data.Entity
{
    [Table("ProductionMasters", Schema = "PROD")]
    public class ProductionMaster:Base
    {
        [MaxLength(150)]
        public string productionNo { get; set; }

        public DateTime? productionDate { get; set; }

        public int? bOMId { get; set; }
        public BOMMaster bOM { get; set; }
        
        public decimal? bomQty { get; set; }

        public int? batchId { get; set; }
        public ProductionBatch batch { get; set; }

        [MaxLength(350)]
        public string remarks { get; set; }

        public int? supplierId { get; set; }
        public Organization supplier { get; set; }

        [MaxLength(50)]
        public string operationType { get; set; }

        public int? isClose { get; set; }

        [NotMapped]
        public string bomItem { get; set; }
        [NotMapped]
        public string supplierName { get; set; }

        public int? isStockClose { get; set; }
    }
}
