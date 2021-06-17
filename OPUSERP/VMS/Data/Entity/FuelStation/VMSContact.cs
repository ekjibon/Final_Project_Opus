using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.Master;

namespace OPUSERP.VMS.Data.Entity.FuelStation
{
    [Table("VMSContact", Schema = "VMS")]
    public class VMSContact:Base
    {
        public int? fuelStationInfoId { get; set; }
        public FuelStationInfo fuelStationInfo { get; set; }
        public int? resourceID { get; set; }
        public VMSResource resource { get; set; }
    }
}
