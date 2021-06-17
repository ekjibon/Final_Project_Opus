using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("PreferableVendors", Schema = "SCM")]
    public class PreferableVendor:Base
    {
        public int requisitionDetailId { get; set; }
        public RequisitionDetail requisitionDetail { get; set; }

        public int? organizationId { get; set; }
        public Organization organization { get; set; }
    }
}
