using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Supplier
{
    [Table("SupplierItemInfo", Schema = "SCM")]
    public class SupplierItemInfo:Base
    {
        public int? organizationId { get; set; }
        public Organization organization { get; set; }

        public int? itemId { get; set; }
        public Item item { get; set; }
    }
}
