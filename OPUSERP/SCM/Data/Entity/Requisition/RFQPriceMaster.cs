//using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RFQPriceMaster", Schema = "SCM")]
    public class RFQPriceMaster:Base
    {
        public int? rFQMasterId { get; set; }
        public RFQMaster rFQMaster { get; set; }
        public int? supplierId { get; set; }
        public Organization supplier { get; set; }
    }
}
