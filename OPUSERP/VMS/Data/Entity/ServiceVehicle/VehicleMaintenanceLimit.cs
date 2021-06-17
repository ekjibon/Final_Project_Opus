using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VehicleMaintenanceLimit", Schema = "VMS")]
    public class VehicleMaintenanceLimit:Base
    {
        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        

    }
}
