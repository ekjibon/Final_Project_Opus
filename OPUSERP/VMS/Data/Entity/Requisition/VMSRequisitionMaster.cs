using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Requisition
{
    [Table("VMSRequisitionMaster", Schema = "VMS")]
    public class VMSRequisitionMaster:Base
    {
        public string reqNo { get; set; }

        public string reqBy { get; set; }

        public string reqDate { get; set; }

        public string reqTime { get; set; }

        public int? vehicleTypeId { get; set; }
        public VehicleType vehicleType { get; set; }

        public string vehicleCondition { get; set; }

        public string vehicleSeat { get; set; }

        public int? districtId { get; set; }
        public District district { get; set; }

    }
}
