using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.FuelStation;

namespace OPUSERP.VMS.Data.Entity.Requisition
{
    [Table("RequisitionComment", Schema = "VMS")]
    public class RequisitionComment: Base
    {
        public int? requisitionMasterId { get; set; }
        public VMSRequisitionMaster requisitionMaster { get; set; }

        [MaxLength(250)]
        public string titles { get; set; }
        public string comments { get; set; }
    }
}
