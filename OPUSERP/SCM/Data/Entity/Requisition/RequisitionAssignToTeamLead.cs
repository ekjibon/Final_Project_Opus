using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RequisitionAssignToTeamLeads", Schema = "SCM")]
    public class RequisitionAssignToTeamLead:Base
    {
        public int? requisitionMasterId { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }
        public int? requisitionDetailId { get; set; }
        public RequisitionDetail requisitionDetail { get; set; }
        public DateTime assignDate { get; set; }
        public int? assignToUserId { get; set; }
    }
}
