using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Stock
{
    [Table("ItemReqDetails", Schema = "SCM")]
    public class ItemReqDetails:Base
    {
        public int? itemReqMasterId { get; set; }
        public ItemReqMaster itemReqMaster { get; set; }

        public int? itemspecificationId { get; set; }
        public ItemSpecification itemspecification { get; set; }

        public decimal? quantity { get; set; }
    }
}
