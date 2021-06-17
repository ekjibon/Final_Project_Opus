using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RFQReqDetail", Schema = "SCM")]
    public class RFQReqDetail: Base
    {
        public int? rFQMasterId { get; set; }
        public RFQMaster rFQMaster { get; set; }
        public int? requisitionMasterId { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }

        public int? requisitionDetailId { get; set; }
        public RequisitionDetail requisitionDetail { get; set; }

        public string LotNo { get; set; }
       

    }
}
