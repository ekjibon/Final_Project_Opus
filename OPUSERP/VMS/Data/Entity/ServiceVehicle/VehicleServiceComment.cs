using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VehicleServiceComment", Schema = "VMS")]
    public class VehicleServiceComment : Base
    {
        public int? vehicleServiceHistoryId { get; set; }
        public VehicleServiceHistory vehicleServiceHistory { get; set; }

        [MaxLength(250)]
        public string titles { get; set; }
        public string comments { get; set; }
    }
}
