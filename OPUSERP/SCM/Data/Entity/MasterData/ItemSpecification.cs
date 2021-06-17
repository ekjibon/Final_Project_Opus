using OPUSERP.Data.Entity;
using OPUSERP.POS.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("ItemSpecification", Schema = "SCM")]
    public class ItemSpecification :Base
    {
        public int? itemId { get; set; }
        public Item Item { get; set; }

        public string SKUNumber { get; set; }

        [MaxLength(250)]
        public string specificationName { get; set; }
        public string specImageUrl { get; set; }

        public int? reOrderLevel { get; set; }
        public string description { get; set; }

        public int? unitId { get; set; }
        public Unit unit { get; set; }

        public int? storeId { get; set; }
        public Store store { get; set; }

        public int? isQR { get; set; }
        public int? isMostUsed { get; set; }
        public int? isBuild { get; set; }
    }
}
