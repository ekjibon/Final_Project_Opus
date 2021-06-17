using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Requisition
{
    [Table("VMSRequisitionDetails", Schema = "VMS")]
    public class VMSRequisitionDetails:Base
    {
        public int? requisitionId { get; set; }
        public VMSRequisitionMaster requisition { get; set; }

        public string routingPlace { get; set; }

        public int? sequenseNo { get; set; }
    }
}
